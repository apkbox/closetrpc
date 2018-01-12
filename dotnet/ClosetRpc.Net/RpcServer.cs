// --------------------------------------------------------------------------------
// <copyright file="RpcServer.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Server type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    using ClosetRpc.Protocol;

    using Common.Logging;

    /// <summary>
    /// RPC server.
    /// </summary>
    public class RpcServer
    {
        #region Fields

        private readonly List<ServerContext> activeConnections = new List<ServerContext>();

        private readonly object activeConnectionsLock = new object();

        private readonly Lazy<IProtocolObjectFactory> cachedFactory;

        private readonly ILog log = LogManager.GetLogger<RpcServer>();

        private readonly GlobalObjectManager objectManager = new GlobalObjectManager();

        private readonly object stateLock = new object();

        private readonly IServerTransport transport;

        private bool isRunning;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes the RPC server using specified transport.
        /// </summary>
        /// <param name="transport">Transport instance.</param>
        public RpcServer(IServerTransport transport)
        {
            this.transport = transport;
            this.cachedFactory = new Lazy<IProtocolObjectFactory>(
                this.CreateProtocolObjectFactory,
                LazyThreadSafetyMode.ExecutionAndPublication);
            this.EventSource = new GlobalEventSource(this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets an event source that allows to send an event to all active connections.
        /// </summary>
        public IEventSource EventSource { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a protocol object factory instance that is used to construct
        /// and read RPC protocol messages.
        /// </summary>
        protected IProtocolObjectFactory ProtocolObjectFactory
        {
            get
            {
                return this.cachedFactory.Value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets snapshot of active connection contexts.
        /// </summary>
        /// <returns>
        /// Collection of active connection contexts.
        /// </returns>
        /// <remarks>
        /// The function allows to obtain local event source
        /// to send events to a specific client.
        /// Note that to send event to all clients <see cref="EventSource"/> is
        /// more efficient.
        /// </remarks>
        public IEnumerable<IServerContext> GetActiveConnections()
        {
            lock (this.activeConnectionsLock)
            {
                return this.activeConnections.ToList();
            }
        }

        /// <summary>
        /// Registers a service instance.
        /// </summary>
        /// <param name="service">Service instance.</param>
        public void RegisterService(IRpcService service)
        {
            this.log.InfoFormat(
                "Registered service '{0}' with name '{1}'",
                service.GetType().AssemblyQualifiedName,
                service.Name);
            this.objectManager.RegisterService(service);
        }

        /// <summary>
        /// Registers a service instance with a custom interface name.
        /// </summary>
        /// <param name="service">Service instance.</param>
        /// <param name="serviceName">Service interface name.</param>
        public void RegisterService(IRpcServiceStub service, string serviceName)
        {
            this.log.InfoFormat(
                "Registered service stub '{0}' with name '{1}'",
                service.GetType().AssemblyQualifiedName,
                serviceName);
            this.objectManager.RegisterService(service, serviceName);
        }

        /// <summary>
        /// Starts synchronously listening for incoming connections.
        /// </summary>
        /// <remarks>
        /// The function quits when <see cref="Shutdown"/> is called.
        /// </remarks>
        public void Run()
        {
            this.log.Info("Running...");
            lock (this.stateLock)
            {
                // TODO: Problem here is that when Shutdown is called, it is,
                // while holding stateLock sets isRunning to false.
                // Now if Run was just called, and waiting for stateLock,
                // once shutdown complete, Run will set isRunning true again
                // and when attempt to use socket throw an exception.
                this.isRunning = true;
            }

            while (this.isRunning)
            {
                IChannel channel;
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
                    // TODO: This is not scalable to create thread for each connection.
                    // Instead a thread pool and a non-blocking transport design
                    // should be used. If transport's underlying implmentation does not
                    // allow non-blocking scenarios, then transport should implement
                    // if using threads, which in turn makes scenarios that use these
                    // transports makes it unscalable.
                    this.log.Debug("Connection accepted. Creating connection context and thread.");
                    var thread = new Thread(this.ConnectionThread);
                    thread.Name = "RpcServerConnectionThread";
                    var context = new ServerContext(this, channel, thread);
                    lock (this.activeConnectionsLock)
                    {
                        this.activeConnections.Add(context);
                    }

                    context.Thread.Start(context);
                }
            }

            lock (this.stateLock)
            {
                lock (this.activeConnectionsLock)
                {
                    foreach (var connection in this.activeConnections)
                    {
                        connection.Channel.Close();

                        // TODO: This is a deadlock situation as the connection
                        // thread will stuck waiting for the lock when terminating
                        // to remove itself from list of active connections.

                        // connection.Thread.Join();
                    }
                }

                this.isRunning = false;
                Monitor.PulseAll(this.stateLock);
            }

            this.log.Info("Stopped.");
        }

        /// <summary>
        /// Shutdown the server, optionally waiting for all pending requests to complete.
        /// </summary>
        /// <param name="waitForCompletion">
        /// When true, wait for all pending requests to complete.
        /// </param>
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

        internal void SendEvent(RpcCallParameters callParameters, IChannel channel)
        {
            var cachedCall = this.ProtocolObjectFactory.BuildCall(callParameters);
            if (channel != null)
            {
                this.ProtocolObjectFactory.WriteMessage(channel.Stream, 0, cachedCall, null);
            }
            else
            {
                lock (this.activeConnectionsLock)
                {
                    foreach (var c in this.activeConnections)
                    {
                        this.ProtocolObjectFactory.WriteMessage(c.Channel.Stream, 0, cachedCall, null);
                    }
                }
            }
        }

        /// <summary>
        /// Creates an instance of protocol object factory.
        /// </summary>
        /// <returns>A new protocol object factory instance.</returns>
        /// <remarks>
        /// The returned instance is cached. The derived class can override
        /// this method to implement custom control protocol.
        /// </remarks>
        protected virtual IProtocolObjectFactory CreateProtocolObjectFactory()
        {
            return new ProtocolObjectFactory();
        }

        private void ConnectionThread(object param)
        {
            var context = (ServerContext)param;
            this.log.DebugFormat("Connection established. Thread={0}", context.Thread.ManagedThreadId);
            while (this.isRunning)
            {
                try
                {
                    this.ProcessSingleMessage(context, context.Channel.Stream);
                }
                catch (IOException ex)
                {
                    this.log.Error("Exception while processing message.", ex);
                    break;
                }
            }

            lock (this.activeConnectionsLock)
            {
                this.activeConnections.Remove(context);
            }

            this.log.DebugFormat("Connection terminated. Thread={0}", context.Thread.ManagedThreadId);
        }

        private void ExecuteRequest(ServerContext context, IRpcCall call, IRpcResult result)
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
                    this.log.ErrorFormat(
                        "Cannot find object '{0}' with '{1}.{2}' interface",
                        call.ObjectId,
                        call.ServiceName,
                        call.MethodName);
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
            this.log.TraceFormat("Received message {0}.", requestMessage.RequestId);
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
            // TODO: Intercept exceptions that happened during execution
            // and deal with them separately. The most important reason is that
            // if service happen to throw IOException or SocketException these
            // are not misinterpreted as related to RPC communication.
            this.ExecuteRequest(context, callRequest, resultResponse);

            // Reply if necessary
            if (!callRequest.IsAsync)
            {
                protocolObjectFactory.WriteMessage(stream, requestMessage.RequestId, null, resultResponse);
                this.log.TraceFormat("Sent reply {0}.", requestMessage.RequestId);
            }
        }

        #endregion
    }
}
