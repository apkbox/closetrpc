// --------------------------------------------------------------------------------
// <copyright file="Client.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Client type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using ClosetRpc.Net.Protocol;

    public class Client
    {
        #region Fields

        private readonly Lazy<IProtocolObjectFactory> cachedFactory;

        private readonly Channel channel;

        private readonly Dictionary<uint, PendingCall> pendingCalls = new Dictionary<uint, PendingCall>();

        private readonly object pendingCallsLock = new object();

        private readonly object receiveThreadInitLock = new object();

        private readonly object requestIdLock = new object();

        private bool isRunning;

        private uint lastRequestId;

        private Thread receiverThread;

        #endregion

        #region Constructors and Destructors

        public Client(IClientTransport transport)
        {
            this.channel = transport.Connect();
            this.cachedFactory = new Lazy<IProtocolObjectFactory>(this.CreateProtocolObjectFactory);
        }

        #endregion

        #region Properties

        protected IProtocolObjectFactory ProtocolObjectFactory => this.cachedFactory.Value;

        #endregion

        #region Public Methods and Operators

        public IRpcResult CallService(IRpcCallBuilder rpcCallBuilder)
        {
            uint requestId;
            lock (this.requestIdLock)
            {
                requestId = ++this.lastRequestId;
            }

            this.SendRequest(requestId, rpcCallBuilder);
            return this.AwaitResult(requestId);
        }

        public IRpcCallBuilder CreateCallBuilder()
        {
            return this.ProtocolObjectFactory.CreateCallBuilder();
        }

        public void Shutdown()
        {
            this.isRunning = false;
            lock (this.pendingCallsLock)
            {
                Monitor.PulseAll(this.pendingCallsLock);
            }

            var thread = this.receiverThread;
            thread?.Join();
        }

        #endregion

        #region Methods

        protected virtual IProtocolObjectFactory CreateProtocolObjectFactory()
        {
            return new ProtocolObjectFactory();
        }

        private IRpcResult AwaitResult(uint requestId)
        {
            this.EnsureReceiverThread();

            lock (this.pendingCallsLock)
            {
                this.pendingCalls[requestId].Status = PendingCallStatus.AwaitingResult;

                while (true)
                {
                    if (this.pendingCalls.TryGetValue(requestId, out var pendingCall))
                    {
                        if (pendingCall.Status == PendingCallStatus.Received)
                        {
                            var result = pendingCall.Result;
                            this.pendingCalls.Remove(requestId);
                            return result;
                        }
                        else if (pendingCall.Status == PendingCallStatus.Cancelled)
                        {
                            this.pendingCalls.Remove(requestId);
                            return null;
                        }
                    }

                    Monitor.Wait(this.pendingCallsLock);
                }
            }
        }

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

        private bool ProcessSingleMessage()
        {
            var message = this.ProtocolObjectFactory.RpcMessageFromStream(this.channel.Stream);

            // If call portion of the message is filled, then it is an event.
            if (message.Call != null)
            {
                // Check first if anyone expects the event
                Debug.Fail("Not yet implemented");

                /*
                                // Check first if anyone expects the event
                                auto event_service = event_service_manager_.GetService(response->call().service());
                                if (event_service != nullptr) {
                                  std::lock_guard<std::mutex> lock(event_queue_mtx_);
                                  // TODO: Implement queue size limiting. Drop the oldest events.
                                  // BUG: Although it looks smart to cache the handler
                                  // pointer to avoid extra lookup it is a bad move.
                                  // Between time we received event and user called
                                  // PumpEvent, the other thread coild have called StopListening
                                  // so, the pointer to the service will be invalid.
                                  // On the other hand StopListening should reliably remove
                                  // events that are not being listened.
                                  event_queue_.emplace_back(event_service, std::move(response));
                                  event_pending_cv_.notify_all();
                                }
                                 */
            }
            else
            {
                lock (this.pendingCallsLock)
                {
                    if (!this.pendingCalls.TryGetValue(message.RequestId, out var pendingCall))
                    {
                        return false;
                    }

                    pendingCall.Result = message.Result;
                    pendingCall.Status = PendingCallStatus.Received;

                    Monitor.PulseAll(this.pendingCallsLock);
                }
            }

            return true;
        }

        private void ReceiverThread()
        {
            this.isRunning = true;

            // If channel disconnected - stop processing messages until it comes back.
            while (!this.isRunning)
            {
                try
                {
                    this.ProcessSingleMessage();
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                }
            }
        }

        private void SendRequest(uint requestId, IRpcCallBuilder callBuilder)
        {
            if (!callBuilder.IsAsync)
            {
                this.pendingCalls.Add(requestId, new PendingCall());
            }

            using (var ostream = new BufferedStream(this.channel.Stream, 2048))
            {
                this.ProtocolObjectFactory.WriteMessage(ostream, requestId, callBuilder, null);
            }
        }

        #endregion
    }
}
