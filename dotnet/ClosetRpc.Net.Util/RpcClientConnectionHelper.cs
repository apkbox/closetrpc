// --------------------------------------------------------------------------------
// <copyright file="RpcClientConnectionHelper.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the BindingStatus type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Util
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Threading;

    using Common.Logging;

    /// <summary>
    /// Connects.
    /// </summary>
    public class RpcClientConnectionHelper
    {
        #region Fields

        private readonly RpcClient client;

        private readonly ILog log = LogManager.GetLogger<RpcClientConnectionHelper>();

        private readonly object stateLock = new object();

        private bool isRunning;

        private State state = State.NotBound;

        private Thread thread;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcClientConnectionHelper"/> class.
        /// </summary>
        /// <param name="client">RpcClient instance.</param>
        public RpcClientConnectionHelper(RpcClient client)
        {
            this.client = client;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when binding bindingStatus changes.
        /// </summary>
        public event EventHandler<BindingStatusChangedEventArgs> BindingChanged;

        #endregion

        #region Enums

        internal enum State
        {
            NotBound,

            Binding,

            Bound,

            Unbinding,
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the RPC client is bound to the remote peer.
        /// </summary>
        public bool IsBound { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Starts binding to the remote peer asynchronously.
        /// </summary>
        /// <remarks>
        /// After call of this function the class will attempt to connect and
        /// maintain the connection by reconnecting when connection is lost.
        /// </remarks>
        public void Bind()
        {
            bool start = false;
            lock (this.stateLock)
            {
                if (this.state == State.NotBound)
                {
                    this.log.Trace("Not bound. Request binding.");
                    this.state = State.Binding;
                    start = true;
                    if (this.thread != null)
                    {
                        this.log.Error("Invariant failure");
                        Debug.Fail("Invariant failure.");
                    }
                }
            }

            if (start)
            {
                this.log.Trace("Starting event pump thread.");
                this.thread = new Thread(this.EventPumpThread);
                this.thread.Name = "RpcEventPumpThread";
                this.isRunning = true;
                this.thread.Start();
            }
        }

        /// <summary>
        /// Stops current connection or attempts to connect.
        /// </summary>
        public void Unbind()
        {
            var stop = false;
            lock (this.stateLock)
            {
                if (this.state == State.Binding)
                {
                    this.log.Trace("Currently binding, waiting for bound state.");
                    Monitor.Wait(this.stateLock);
                    return;
                }

                if (this.state == State.Bound)
                {
                    this.log.Trace("Currently bound. Unbinding.");
                    this.state = State.Unbinding;
                    stop = true;
                }
            }

            if (stop)
            {
                this.log.Trace("Stopping thread and waiting for threa to join.");
                this.isRunning = false;
                this.thread.Join();
                this.thread = null;
            }
        }

        #endregion

        #region Methods

        private void EventPumpThread()
        {
            this.log.Trace("Starting event pump thread.");

            lock (this.stateLock)
            {
                this.state = State.Bound;
                Monitor.PulseAll(this.stateLock);
            }

            this.log.Trace("State is bound.");

            while (this.isRunning)
            {
                try
                {
                    this.log.Trace("Connecting...");
                    this.client.Connect();
                    this.log.Trace("Connected.");
                    this.SetBindingStatus(true);
                }
                catch (SocketException)
                {
                    this.log.Trace("Failed to connect - sleeping and retrying.");
                    Thread.Sleep(1000);
                    continue;
                }

                this.log.Trace("Running...");
                while (this.isRunning && this.client.IsConnected)
                {
                    this.client.WaitForEvents(500);
                    if (!this.isRunning)
                    {
                        break;
                    }

                    this.client.PumpEvents();
                }

                this.log.Trace("Disconnected or stopping...");

                if (!this.client.IsConnected)
                {
                    this.log.Trace("Actually - disconnected.");
                    this.SetBindingStatus(false);
                }
            }

            this.SetBindingStatus(false);

            this.log.Trace("Shutting down client.");
            this.client.Shutdown(true);

            lock (this.stateLock)
            {
                this.state = State.NotBound;
            }

            this.log.Trace("Exiting event pump thread.");
        }

        private void SetBindingStatus(bool bound)
        {
            if (this.IsBound == bound)
            {
                return;
            }

            this.log.DebugFormat("Binding status changed. IsBound={0}", bound);
            this.IsBound = bound;

            var handler = this.BindingChanged;
            if (handler != null)
            {
                handler.Invoke(this, new BindingStatusChangedEventArgs(bound));
            }
        }

        #endregion
    }
}
