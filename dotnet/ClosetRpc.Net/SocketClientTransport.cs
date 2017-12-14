// --------------------------------------------------------------------------------
// <copyright file="SocketClientTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SocketClientTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System.Net.Sockets;

    public class SocketClientTransport : IClientTransport
    {
        #region Fields

        private readonly string hostname;

        private readonly int port;

        #endregion

        #region Constructors and Destructors

        public SocketClientTransport(string hostname, int port)
        {
            this.hostname = hostname;
            this.port = port;
        }

        #endregion

        #region Public Methods and Operators

        public Channel Connect()
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(this.hostname, this.port);
            return new Channel(tcpClient);
        }

        #endregion
    }
}
