// --------------------------------------------------------------------------------
// <copyright file="RpcClient.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Client type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using ClosetRpc.Protocol;

    using Common.Logging;

    /// <summary>
    /// RPC client.
    /// </summary>
    public class RpcClient
    {
        #region Fields

        private readonly Lazy<IProtocolObjectFactory> cachedFactory;

        private readonly object channelLock = new object();

        private readonly ConcurrentQueue<Action> eventQueue = new ConcurrentQueue<Action>();

        private readonly object eventQueueLock = new object();

        private readonly EventServiceManager eventServiceManager = new EventServiceManager();

        private readonly ILog log = LogManager.GetLogger<RpcClient>();

        private readonly Dictionary<uint, PendingCall> pendingCalls = new Dictionary<uint, PendingCall>();

        private readonly object pendingCallsLock = new object();

        private readonly object receiveThreadInitLock = new object();

        private readonly object requestIdLock = new object();

        private readonly IClientTransport transport;

        private IChannel channel;

        private bool isRunning;

        private uint lastRequestId;

        private Thread receiverThread;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes RPC client instance.
        /// </summary>
        /// <param name="transport">Transport instance.</param>
        public RpcClient(IClientTransport transport)
        {
            this.transport = transport;
            this.log.Debug("Client created.");
            this.cachedFactory = new Lazy<IProtocolObjectFactory>(this.CreateProtocolObjectFactory);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the client is connected to the remote peer.
        /// </summary>
        public bool IsConnected { get; private set; }

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
        /// Add an event handler that allows to listen to events sent by the remote peer.
        /// </summary>
        /// <param name="handler">Handler instance.</param>
        public void AddEventListener(IEventHandler handler)
        {
            this.eventServiceManager.RegisterHandler(handler);
        }

        /// <summary>
        /// Add and event handler that allows to listen to events sent by the remote peer.
        /// </summary>
        /// <param name="handler">Handler instance.</param>
        /// <param name="eventInterfaceName">Event interface name.</param>
        public void AddEventListener(IEventHandlerStub handler, string eventInterfaceName)
        {
            this.eventServiceManager.RegisterHandler(handler, eventInterfaceName);
        }

        /// <summary>
        /// Sends request to the server.
        /// </summary>
        /// <param name="rpcCallParameters">Call parameters.</param>
        /// <returns>
        /// <para>
        /// Returns <see cref="RpcStatus.Succeeded"/> on success or error status
        /// on failure.
        /// </para>
        /// <para>
        /// Asynchronous call success indicates that request was sent successfully
        /// and <see cref="IRpcResult.ResultData"/> is unfilled.
        /// </para>
        /// <para>
        /// Synchronous call success indicates that the request was sent and reply
        /// received successfully and <see cref="IRpcResult.ResultData"/> contains
        /// received response.
        /// </para>
        /// </returns>
        public IRpcResult CallService(RpcCallParameters rpcCallParameters)
        {
            this.log.DebugFormat(
                "Calling service {0}.{1} (async:{2})",
                rpcCallParameters.ServiceName,
                rpcCallParameters.MethodName,
                rpcCallParameters.IsAsync);

            this.EnsureConnection();

            uint requestId;
            lock (this.requestIdLock)
            {
                requestId = ++this.lastRequestId;
            }

            this.log.TraceFormat("Request ID {0}", requestId);

            IRpcResult result;
            if (rpcCallParameters.IsAsync)
            {
                result = this.SendAsyncRequest(requestId, rpcCallParameters);
            }
            else
            {
                result = this.SendSyncRequest(requestId, rpcCallParameters);
                if (result == null)
                {
                    return this.AwaitResult(requestId);
                }
            }

            if (result == null)
            {
                result = this.ProtocolObjectFactory.CreateRpcResult();
                result.Status = RpcStatus.Succeeded;
            }

            return result;
        }

        /// <summary>
        /// Connects to the remote peer synchronously.
        /// </summary>
        public void Connect()
        {
            this.log.Trace("Connecting.");
            lock (this.channelLock)
            {
                if (this.IsConnected)
                {
                    this.log.Trace("Already connected.");
                    return;
                }

                this.log.Trace("Creating channel.");
                this.channel = this.transport.Connect();
                this.IsConnected = true;
                Monitor.PulseAll(this.channelLock);
                this.log.Trace("Connected.");
            }

            this.EnsureReceiverThread();
        }

        /// <summary>
        /// Pumps available events from the by and calls appropriate event handler.
        /// </summary>
        /// <returns>Returns true if there are more events in the queue or false
        /// if event queue is empty.</returns>
        public bool PumpEvents()
        {
            Action pendingEvent;
            if (!this.eventQueue.TryDequeue(out pendingEvent))
            {
                return false;
            }

            pendingEvent();

            return !this.eventQueue.IsEmpty;
        }

        /// <summary>
        /// Shutdowns the client, optionally waiting for all pending requests to complete.
        /// </summary>
        /// <param name="waitForCompletion">
        /// When true, wait for all pending requests to complete.
        /// </param>
        /// <remarks>
        /// This method can be called from any thread.
        /// If <paramref name="waitForCompletion"/> is true, then method blocks
        /// until all outstanding requests complete.
        /// All requests made after this method is called will fail.
        /// Incoming events will be ignored and those pending in the queue
        /// will be discarded.
        /// </remarks>
        public void Shutdown(bool waitForCompletion)
        {
            if (waitForCompletion)
            {
                // TODO: do actual implementation
                // throw new NotImplementedException();
            }

            this.isRunning = false;
            this.CloseChannel();

            lock (this.pendingCallsLock)
            {
                Monitor.PulseAll(this.pendingCallsLock);
            }

            lock (this.eventQueueLock)
            {
                Monitor.PulseAll(this.eventQueueLock);
            }

            lock (this.receiveThreadInitLock)
            {
                var thread = this.receiverThread;
                if (thread != null)
                {
                    thread.Join();
                }

                this.receiverThread = null;
            }
        }

        // TODO: Replace with a method that returns WaitHandle.
        /// <summary>
        /// Waits for events for specified amount of time.
        /// </summary>
        /// <param name="millisecondsTimeout">Timeout in milliseconds.</param>
        /// <returns>Returns true if events are available, or false if event queue is empty or
        /// client is not connected.</returns>
        public bool WaitForEvents(int millisecondsTimeout)
        {
            return this.WaitForEvents(new TimeSpan(0, 0, 0, 0, millisecondsTimeout));
        }

        /// <summary>
        /// Waits for events.
        /// </summary>
        /// <returns>Returns true if events are available, or false if event queue is empty or
        /// client is not connected.</returns>
        public bool WaitForEvents()
        {
            return this.WaitForEvents(-1);
        }

        /// <summary>
        /// Waits for events.
        /// </summary>
        /// <param name="timeout">Timeout amount.</param>
        /// <returns>Returns true if events are available, or false if event queue is empty or
        /// client is not connected.</returns>
        public bool WaitForEvents(TimeSpan timeout)
        {
            if (!this.isRunning)
            {
                return false;
            }

            lock (this.eventQueueLock)
            {
                if (!this.eventQueue.IsEmpty)
                {
                    return true;
                }

                Monitor.Wait(this.eventQueueLock, timeout);
                if (!this.eventQueue.IsEmpty)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Methods

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

        /// <summary>
        /// Called when client is disconnected from server.
        /// </summary>
        protected virtual void OnDisconnected()
        {
        }

        private void AbortPendingCalls(RpcStatus reason)
        {
            this.log.Debug("Aborting all pending calls.");
            Debug.Assert(Monitor.IsEntered(this.pendingCallsLock), "Pending calls lock must be held.");

            foreach (var pendingCall in this.pendingCalls)
            {
                if (pendingCall.Value.Result == null)
                {
                    pendingCall.Value.Result = this.ProtocolObjectFactory.CreateRpcResult();
                }

                pendingCall.Value.Result.Status = reason;
                pendingCall.Value.Status = PendingCallStatus.Cancelled;
            }
        }

        private IRpcResult AwaitResult(uint requestId)
        {
            this.EnsureReceiverThread();

            lock (this.pendingCallsLock)
            {
                this.log.TraceFormat("Waiting for reply on request {0}.", requestId);
                while (true)
                {
                    PendingCall pendingCall;
                    if (this.pendingCalls.TryGetValue(requestId, out pendingCall))
                    {
                        if (pendingCall.Status == PendingCallStatus.Received
                            || pendingCall.Status == PendingCallStatus.Cancelled)
                        {
                            var result = pendingCall.Result;
                            this.pendingCalls.Remove(requestId);
                            this.log.TraceFormat("Found reply on request {0} with status {1}.", requestId, result);
                            return result;
                        }
                    }

                    Monitor.Wait(this.pendingCallsLock);
                }
            }
        }

        private void CloseChannel()
        {
            lock (this.channelLock)
            {
                this.log.Debug("Closing the channel.");
                if (this.channel != null)
                {
                    this.channel.Close();
                    this.channel = null;
                    this.log.Debug("Channel closed.");
                }
                else
                {
                    this.log.Trace("Channel was not open.");
                }

                this.IsConnected = false;
                Monitor.PulseAll(this.channelLock);
            }

            this.OnDisconnected();
        }

        private void EnsureConnection()
        {
            this.Connect();
            this.EnsureReceiverThread();
        }

        /// <summary>
        /// Lazily creates a message receiver thread.
        /// </summary>
        private void EnsureReceiverThread()
        {
            lock (this.receiveThreadInitLock)
            {
                this.log.Trace("Ensuring receiver thread.");
                if (this.receiverThread != null)
                {
                    this.log.Trace("Receiver thread already running.");
                    return;
                }

                this.log.Trace("Creating a new receiver thread.");
                this.receiverThread = new Thread(this.ReceiverThread);
                this.receiverThread.Name = "RpcReceiverThread";
                this.receiverThread.Start();
            }
        }

        private Stream GetChannelStream()
        {
            Stream stream;
            lock (this.channelLock)
            {
                if (!this.IsConnected)
                {
                    throw new IOException("Disconnected.");
                }

                stream = this.channel.Stream;
            }

            return stream;
        }

        private void ReceiveAndHandleSingleMessage()
        {
            this.log.Trace("Waiting for message");
            var stream = this.GetChannelStream();
            var message = this.ProtocolObjectFactory.RpcMessageFromStream(stream);
            this.log.TraceFormat("Received message {0}.", message.RequestId);

            // If call portion of the message is filled, then it is an event.
            if (message.Call != null)
            {
                if (!message.Call.IsAsync)
                {
                    this.log.WarnFormat(
                        "Incoming message '{0}.{1}' is an event, but not marked as asynchronous.",
                        message.Call.ServiceName,
                        message.Call.MethodName);
                }

                // Check first if anyone expects the event
                var eventService = this.eventServiceManager.GetHandler(message.Call.ServiceName);
                if (eventService != null)
                {
                    lock (this.eventQueueLock)
                    {
                        this.eventQueue.Enqueue(() => eventService.CallMethod(message.Call));
                        Monitor.PulseAll(this.eventQueueLock);
                    }
                }
                else
                {
                    this.log.DebugFormat(
                        "Unexpected event '{0}.{1}'",
                        message.Call.ServiceName,
                        message.Call.MethodName);
                }
            }
            else if (message.Result != null)
            {
                lock (this.pendingCallsLock)
                {
                    PendingCall pendingCall;
                    if (!this.pendingCalls.TryGetValue(message.RequestId, out pendingCall))
                    {
                        this.log.WarnFormat("Pending message {0} not found. Message ignored.", message.RequestId);
                        return;
                    }

                    pendingCall.Result = message.Result;
                    pendingCall.Status = PendingCallStatus.Received;

                    Monitor.PulseAll(this.pendingCallsLock);
                }
            }
            else
            {
                this.log.WarnFormat("Message {0} contains neither call nor reply.", message.RequestId);
            }
        }

        private void ReceiverThread()
        {
            this.isRunning = true;
            this.log.Debug("Receiver thread started.");

            // If channel disconnected - stop processing messages until it comes back.
            while (this.isRunning)
            {
                try
                {
                    this.ReceiveAndHandleSingleMessage();
                }
                catch (IOException ex)
                {
                    this.log.Debug("Exception in receiver thread. Disconnected?", ex);
                    this.CloseChannel();
                    lock (this.pendingCallsLock)
                    {
                        this.AbortPendingCalls(RpcStatus.ChannelFailure);
                        Monitor.PulseAll(this.pendingCallsLock);
                    }

                    if (this.isRunning)
                    {
                        lock (this.channelLock)
                        {
                            Monitor.Wait(this.channelLock, 3000);
                        }
                    }
                }
            }

            this.log.Debug("Receiver thread exited.");
        }

        /// <summary>
        /// Sends asynchronous request to the server.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        /// <param name="callParameters">Call parameters.</param>
        /// <returns>
        /// Returns <c>null</c> on success or result containing error status
        /// on failure.
        /// </returns>
        private IRpcResult SendAsyncRequest(uint requestId, RpcCallParameters callParameters)
        {
            if (!callParameters.IsAsync)
            {
                throw new InvalidOperationException("Call is not asychronous.");
            }

            IRpcResult result = null;
            try
            {
                var stream = this.GetChannelStream();
                var call = this.ProtocolObjectFactory.BuildCall(callParameters);
                this.ProtocolObjectFactory.WriteMessage(stream, requestId, call, null);
                this.log.TraceFormat("Asynchronous message sent {0}.", requestId);
            }
            catch (IOException ex)
            {
                this.log.DebugFormat("Exception while sending a message {0}. Disconnected?", ex, requestId);
                this.CloseChannel();
                result = this.ProtocolObjectFactory.CreateRpcResult();
                result.Status = RpcStatus.ChannelFailure;
            }

            return result;
        }

        /// <summary>
        /// Sends specified request to the server.
        /// </summary>
        /// <param name="requestId">Request ID.</param>
        /// <param name="callParameters">Call parameters.</param>
        /// <returns>
        /// Returns <c>null</c> on success or result containing error status
        /// on failure.
        /// </returns>
        private IRpcResult SendSyncRequest(uint requestId, RpcCallParameters callParameters)
        {
            if (callParameters.IsAsync)
            {
                throw new InvalidOperationException("Call must be synchronous.");
            }

            lock (this.pendingCallsLock)
            {
                this.pendingCalls.Add(requestId, new PendingCall());
                this.log.TraceFormat("Registered request {0} to wait for reply.", requestId);
            }

            IRpcResult result = null;

            try
            {
                var stream = this.GetChannelStream();
                var call = this.ProtocolObjectFactory.BuildCall(callParameters);
                this.ProtocolObjectFactory.WriteMessage(stream, requestId, call, null);
                this.log.TraceFormat("Message sent {0}.", requestId);
            }
            catch (IOException ex)
            {
                this.log.Debug("Exception while sending a message. Disconnected?", ex);
                this.CloseChannel();
                lock (this.pendingCallsLock)
                {
                    if (!this.pendingCalls.ContainsKey(requestId))
                    {
                        // This should really never happen, unless there is a logic error.
                        result = this.ProtocolObjectFactory.CreateRpcResult();
                        result.Status = RpcStatus.InternalError;
                        this.log.ErrorFormat("Failed to find pending call {0}.", requestId);
                        Debug.Fail("Failed to find pending call.");
                    }
                    else
                    {
                        this.pendingCalls.Remove(requestId);
                    }
                }

                result = this.ProtocolObjectFactory.CreateRpcResult();
                result.Status = RpcStatus.ChannelFailure;
            }

            return result;
        }

        #endregion
    }
}
