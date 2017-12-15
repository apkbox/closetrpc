// --------------------------------------------------------------------------------
// <copyright file="Server.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Server type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    using ClosetRpc.Net.Protocol;

    using Common.Logging;

    public class Server
    {
        #region Fields

        private readonly IList<ServerContext> activeConnections = new List<ServerContext>();

        private readonly Lazy<IProtocolObjectFactory> cachedFactory;

        private readonly ILog log = LogManager.GetLogger<Server>();

        private readonly GlobalObjectManager objectManager = new GlobalObjectManager();

        private readonly object stateLock = new object();

        private readonly IServerTransport transport;

        private bool isRunning;

        #endregion

        #region Constructors and Destructors

        public Server(IServerTransport transport)
        {
            this.transport = transport;
            this.cachedFactory = new Lazy<IProtocolObjectFactory>(this.CreateProtocolObjectFactory);
        }

        #endregion

        #region Properties

        protected IProtocolObjectFactory ProtocolObjectFactory => this.cachedFactory.Value;

        #endregion

        #region Public Methods and Operators

        public void RegisterService(IRpcService service)
        {
            this.log.InfoFormat(
                "Registered service '{0}' with name '{1}'",
                service.GetType().AssemblyQualifiedName,
                service.Name);
            this.objectManager.RegisterService(service);
        }

        public void RegisterService(IRpcServiceStub service, string serviceName)
        {
            this.log.InfoFormat(
                "Registered service stub '{0}' with name '{1}'",
                service.GetType().AssemblyQualifiedName,
                serviceName);
            this.objectManager.RegisterService(service, serviceName);
        }

        public void Run()
        {
            this.log.Info("Running...");
            lock (this.stateLock)
            {
                this.isRunning = true;
            }

            while (this.isRunning)
            {
                Channel channel;
                try
                {
                    this.log.Trace("Listening for incoming connections...");
                    channel = this.transport.Listen();
                }
                catch (SocketException ex)
                {
                    // Exception is unexpected while we are still running
                    if (this.isRunning)
                    {
                        this.log.ErrorFormat("Socket exception while listening", ex);
                        throw;
                    }

                    this.log.Error("Socket exception while listening", ex);
                    break;
                }

                if (channel != null)
                {
                    this.log.Debug("Creating connection context and thread.");
                    var context = new ServerContext(channel, new Thread(this.ConnectionHandler));
                    this.activeConnections.Add(context);
                    context.Thread.Start(context);
                }
            }

            lock (this.stateLock)
            {
                foreach (var connection in this.activeConnections)
                {
                    connection.Channel.Close();
                    connection.Thread.Join();
                }

                this.isRunning = false;
                Monitor.PulseAll(this.stateLock);
            }

            this.log.Info("Stopped.");
        }

        public void Shutdown(bool waitForCompletion)
        {
            this.log.InfoFormat("Shutdown requested (wait={0}).", waitForCompletion);
            lock (this.stateLock)
            {
                this.isRunning = false;
                this.transport.Cancel();
                if (waitForCompletion)
                {
                    this.log.Debug("Waiting for server shutdown...");
                    Monitor.Wait(this.stateLock);
                }
            }
        }

        #endregion

        #region Methods

        protected virtual IProtocolObjectFactory CreateProtocolObjectFactory()
        {
            return new ProtocolObjectFactory();
        }

        private void ConnectionHandler(object param)
        {
            var context = (ServerContext)param;
            this.log.DebugFormat("Connection established. Thread={0}", context.Thread.ManagedThreadId);
            while (this.isRunning)
            {
                this.ProcessSingleMessage(context, context.Channel.Stream);
            }
        }

        private void ExecuteRequest(ServerContext context, IRpcCall call, IRpcResult result)
        {
            this.log.TraceFormat("Executing request '{0}.{1}'", call.ServiceName, call.MethodName);

            IRpcServiceStub service;

            // Check whether the called method is for service or transient object.
            // The service are identified by name and the lifetime is controlled by the
            // server side.
            // The transient objects are registered as result of a method call and
            // identified by object ID. The lifetime of transient object is
            // controlled by the client.
            if (call.ObjectId != 0)
            {
                this.log.TraceFormat(
                    "Looking for context specific object '{0}' with '{1}.{2}' interface.",
                    call.ObjectId,
                    call.ServiceName,
                    call.MethodName);

                // Try to find object that corresponds to the marshalled interface.
                service = context.ObjectManager.GetInstance(call.ObjectId);
                if (service == null)
                {
                    result.Status = RpcStatus.UnknownInterface;
                    this.log.ErrorFormat(
                        "Cannot find object '{0}' with '{1}.{2}' interface",
                        call.ObjectId,
                        call.ServiceName,
                        call.MethodName);
                }
            }
            else
            {
                this.log.TraceFormat(
                    "Trying to resolve service '{0}.{1}' from global registry.",
                    call.ServiceName,
                    call.MethodName);

                // Try to find service in global scope and then within connection context.
                service = this.objectManager.GetService(call.ServiceName);
                if (service == null)
                {
                    this.log.TraceFormat(
                        "Trying to resolve service '{0}.{1}' from local registry.",
                        call.ServiceName,
                        call.MethodName);

                    service = context.ObjectManager.GetService(call.ServiceName);
                    if (service == null)
                    {
                        this.log.ErrorFormat(
                            "Cannot find service with interface '{0}.{1}'",
                            call.ServiceName,
                            call.MethodName);
                        result.Status = RpcStatus.UnknownInterface;
                    }
                }
            }

            if (service != null)
            {
                this.log.TraceFormat("Calling method '{0}.{1}'.", call.ServiceName, call.MethodName);
                service.CallMethod(context, call, result);
            }
            else
            {
                Debug.Assert(result.Status != RpcStatus.Succeeded, "Result cannot be a success");
            }
        }

        private void ProcessSingleMessage(ServerContext context, Stream stream)
        {
            var protocolObjectFactory = this.ProtocolObjectFactory;

            this.log.Debug("Waiting for incoming message...");

            // Deserialize request
            var requestMessage = protocolObjectFactory.RpcMessageFromStream(stream);
            if (requestMessage.Call == null)
            {
                this.log.Error("No call in the request.");
                return;
            }
            else if (requestMessage.Result != null)
            {
                this.log.Error("Both call and result are in the request.");
                return;
            }

            var callRequest = requestMessage.Call;
            var resultResponse = protocolObjectFactory.CreateRpcResult();

            // Call service handler
            this.ExecuteRequest(context, callRequest, resultResponse);

            // Reply if necessary
            if (!callRequest.IsAsync)
            {
                using (var ostream = new BufferedStream(stream, 2048))
                {
                    protocolObjectFactory.WriteMessage(ostream, requestMessage.RequestId, null, resultResponse);
                }
            }
        }

        #endregion
    }
}
