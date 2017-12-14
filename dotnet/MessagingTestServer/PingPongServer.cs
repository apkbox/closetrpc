// --------------------------------------------------------------------------------
// <copyright file="PingPongServer.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PingPongServer type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessagingTestServer
{
    using ClosetRpc.Net;

    internal class PingPongServer
    {
        #region Fields

        private Server server;

        #endregion

        #region Public Methods and Operators

        public void Run()
        {
            this.server = new Server(new SocketServerTransport(3101));
            this.server.RegisterService(new PingPongService());
            this.server.Run();
        }

        public void Stop()
        {
            this.server.Stop(true);
        }

        #endregion
    }
}
