// --------------------------------------------------------------------------------
// <copyright file="Client.cs" company="Private">
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

    public class Client
    {
        #region Fields

        private readonly Lazy<IProtocolObjectFactory> cachedFactory;

        private readonly object connectLock = new object();

        private readonly ConcurrentQueue<Action> eventQueue = new ConcurrentQueue<Action>();

        private readonly object eventQueueLock = new object();

        private readonly EventServiceManager eventServiceManager = new EventServiceManager();

        private readonly ILog log = LogManager.GetLogger<Client>();

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

        public Client(IClientTransport transport)
        {
            this.transport = transport;
            this.log.Debug("Client created.");
            this.cachedFactory = new Lazy<IProtocolObjectFactory>(this.CreateProtocolObjectFactory);
        }

        #endregion

        #region Properties

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
            this.EnsureConnection();

            uint requestId;
            lock (this.requestIdLock)
            {
                requestId = ++this.lastRequestId;
            }

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

        public void Connect()
        {
            lock (this.connectLock)
            {
                if (this.channel != null)
                {
                    return;
                }

                this.channel = this.transport.Connect();
            }

            this.EnsureReceiverThread();
        }

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
                throw new NotImplementedException();
            }

            this.isRunning = false;
            this.channel.Close();
            this.channel = null;
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

        public bool WaitForEvents()
        {
            while (this.isRunning)
            {
                lock (this.eventQueueLock)
                {
                    if (!this.eventQueue.IsEmpty)
                    {
                        return true;
                    }

                    Monitor.Wait(this.eventQueueLock, 500);
                }
            }

            return false;
        }

        #endregion

        #region Methods

        protected virtual IProtocolObjectFactory CreateProtocolObjectFactory()
        {
            return new ProtocolObjectFactory();
        }

        private void AbortPendingCalls(RpcStatus reason)
        {
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
                            return result;
                        }
                    }

                    Monitor.Wait(this.pendingCallsLock);
                }
            }
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
                if (this.receiverThread != null)
                {
                    return;
                }

                this.receiverThread = new Thread(this.ReceiverThread);
                this.receiverThread.Start();
            }
        }

        private void ReceiveAndHandleSingleMessage()
        {
            var message = this.ProtocolObjectFactory.RpcMessageFromStream(this.channel.Stream);
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
                    lock (this.pendingCallsLock)
                    {
                        this.AbortPendingCalls(RpcStatus.ChannelFailure);
                        Monitor.PulseAll(this.pendingCallsLock);
                    }

                    Thread.Sleep(500);
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
                var call = this.ProtocolObjectFactory.BuildCall(callParameters);
                this.ProtocolObjectFactory.WriteMessage(this.channel.Stream, requestId, call, null);
                this.log.TraceFormat("Asynchronous message sent {0}.", requestId);
            }
            catch (IOException ex)
            {
                this.log.DebugFormat("Exception while sending a message {0}. Disconnected?", ex, requestId);
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
                this.log.TraceFormat("Message {0} pending reply.", requestId);
            }

            IRpcResult result = null;

            try
            {
                var call = this.ProtocolObjectFactory.BuildCall(callParameters);
                this.ProtocolObjectFactory.WriteMessage(this.channel.Stream, requestId, call, null);
                this.log.TraceFormat("Message sent {0}.", requestId);
            }
            catch (IOException ex)
            {
                this.log.Debug("Exception while sending a message. Disconnected?", ex);
                lock (this.pendingCallsLock)
                {
                    if (!this.pendingCalls.ContainsKey(requestId))
                    {
                        this.log.ErrorFormat("Failed to find pending call {0}.", requestId);

                        // TODO: This is really an internal error, so use other status.
                        throw new RpcException(RpcStatus.ChannelFailure);
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
