// --------------------------------------------------------------------------------
// <copyright file="SocketRpcClient.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SocketRpcClient type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Util
{
    using System;
    using System.Net.Sockets;
    using System.Threading;

    public class SocketRpcClient : RpcClient
    {
        #region Fields

        private readonly object bindingLock = new object();

        private bool isRunning;

        private Thread thread;

        #endregion

        #region Constructors and Destructors

        public SocketRpcClient(string hostName, int portNumber)
            : base(new SocketClientTransport(hostName, portNumber))
        {
        }

        #endregion

        #region Public Events

        public event EventHandler<BindingStatusChangedEventArgs> BindingChanged;

        #endregion

        #region Public Properties

        public BindingStatus BindingStatus
        {
            get
            {
                return this.IsConnected ? BindingStatus.Bound : BindingStatus.Unbound;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Bind()
        {
            lock (this.bindingLock)
            {
                if (this.thread == null)
                {
                    this.thread = new Thread(this.WorkerThread);
                    this.thread.Name = "SocketRpcClientThread";
                    this.isRunning = true;
                    this.thread.Start();
                }
            }
        }

        public void Unbind()
        {
            lock (this.bindingLock)
            {
                if (this.thread == null)
                {
                    return;
                }

                this.isRunning = false;
                this.thread.Join();
                this.thread = null;
            }
        }

        #endregion

        #region Methods

        protected override void OnDisconnected()
        {
            base.OnDisconnected();
            this.InvokeBindingStatusChanged(BindingStatus.Unbound);
        }

        private void InvokeBindingStatusChanged(BindingStatus status)
        {
            var handler = this.BindingChanged;
            if (handler != null)
            {
                handler.Invoke(this, new BindingStatusChangedEventArgs(status));
            }
        }

        private void WorkerThread()
        {
            while (this.isRunning)
            {
                try
                {
                    this.Connect();
                    this.InvokeBindingStatusChanged(BindingStatus.Bound);
                }
                catch (SocketException)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                while (this.isRunning)
                {
                    this.WaitForEvents(500);
                    if (!this.isRunning)
                    {
                        break;
                    }

                    this.PumpEvents();
                    if (!this.IsConnected)
                    {
                        break;
                    }
                }
            }

            this.InvokeBindingStatusChanged(BindingStatus.Unbound);
            this.Shutdown(true);
        }

        #endregion
    }
}
