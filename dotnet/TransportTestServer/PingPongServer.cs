// --------------------------------------------------------------------------------
// <copyright file="PingPongServer.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PingPongServer type.
// </summary>
// --------------------------------------------------------------------------------

namespace TransportTestServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    using ClosetRpc.Net;

    public class PingPongServer
    {
        #region Fields

        private readonly List<Tuple<PingPongService, Thread>> services = new List<Tuple<PingPongService, Thread>>();

        private bool stop;

        private SocketServerTransport transport;

        #endregion

        #region Public Methods and Operators

        public void Run()
        {
            this.transport = new SocketServerTransport(3100);

            while (!this.stop)
            {
                try
                {
                    var channel = this.transport.Listen();
                    var service = new PingPongService(channel);
                    var thread = new Thread(service.Run);
                    thread.Start();
                    this.services.Add(new Tuple<PingPongService, Thread>(service, thread));
                }
                catch (SocketException)
                {
                }
                catch (TimeoutException)
                {
                }

                this.services.RemoveAll(o => !o.Item1.IsRunning);
            }

            foreach (var pingPongService in this.services)
            {
                pingPongService.Item1.Stop();
                pingPongService.Item2.Join(1000);
            }
        }

        public void Stop()
        {
            this.stop = true;
            this.transport.Cancel();
        }

        #endregion
    }
}
