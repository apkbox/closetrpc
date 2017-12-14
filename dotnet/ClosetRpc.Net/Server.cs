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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using ClosetRpc.Net.Protocol;

    public class Server
    {
        #region Fields

        private readonly IList<ServerContext> activeConnections = new List<ServerContext>();

        private readonly GlobalObjectManager objectManager = new GlobalObjectManager();

        private readonly object stateLock = new object();

        private readonly IServerTransport transport;

        private bool isRunning;

        #endregion

        #region Constructors and Destructors

        public Server(IServerTransport transport)
        {
            this.transport = transport;
        }

        #endregion

        #region Public Methods and Operators

        public void RegisterService(IRpcService service)
        {
            this.objectManager.RegisterService(service);
        }

        public void RegisterService(IRpcServiceStub service, string serviceName)
        {
            this.objectManager.RegisterService(service, serviceName);
        }

        public void Run()
        {
            lock (this.stateLock)
            {
                this.isRunning = true;
            }

            while (this.isRunning)
            {
                var context = new ServerContext(this.transport.Listen(), new Thread(this.ConnectionHandler));
                this.activeConnections.Add(context);
                context.Thread.Start(context);
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
        }

        public void Stop(bool waitForCompletion)
        {
            lock (this.stateLock)
            {
                this.isRunning = false;
                this.transport.Cancel();
                if (waitForCompletion)
                {
                    Monitor.Wait(this.stateLock);
                }
            }
        }

        #endregion

        #region Methods

        protected virtual IProtocolObjectFactory GetProtocolObjectFactory()
        {
            return new ProtocolObjectFactory();
        }

        private void ConnectionHandler(object param)
        {
            var context = (ServerContext)param;
            while (this.isRunning)
            {
                this.ProcessSingleMessage(context, context.Channel.Stream);
            }
        }

        private void ProcessRequest(ServerContext context, IRpcCall call, IRpcResult result)
        {
            IRpcServiceStub service;

            // Check whether the called method is for service or transient object.
            // The service are identified by name and the lifetime is controlled by the
            // server side.
            // The transient objects are registered as result of a method call and
            // identified by object ID. The lifetime of transient object is
            // controlled by the client.
            if (call.ObjectId != 0)
            {
                // Try to find object that corresponds to the marshalled interface.
                service = context.ObjectManager.GetInstance(call.ObjectId);
                if (service == null)
                {
                    result.Status = RpcStatus.UnknownInterface;
                }
            }
            else
            {
                // Try to find service in global scope and then within connection context.
                service = this.objectManager.GetService(call.ServiceName);
                if (service == null)
                {
                    service = context.ObjectManager.GetService(call.ServiceName);
                    if (service == null)
                    {
                        result.Status = RpcStatus.UnknownInterface;
                    }
                }
            }

            if (service != null)
            {
                service.CallMethod(context, call, result);
            }
            else
            {
                Debug.Assert(result.Status != RpcStatus.Succeeded, "Result cannot be a success");
            }
        }

        private void ProcessSingleMessage(ServerContext context, Stream stream)
        {
            var protocolObjectFactory = this.GetProtocolObjectFactory();

            // Deserialize request
            var requestMessage = protocolObjectFactory.RpcMessageFromStream(stream);

            IRpcCall callRequest = requestMessage.Call;
            var resultResponse = protocolObjectFactory.CreateRpcResult();
            if (callRequest != null)
            {
                // Call service handler
                this.ProcessRequest(context, callRequest, resultResponse);

                // Reply if necessary
                if (!callRequest.IsAsync)
                {
                    protocolObjectFactory.WriteMessage(stream, requestMessage.RequestId, null, resultResponse);
                }
            }
            else
            {
                // TODO: Complain about protocol error
            }
        }

        #endregion
    }
}
