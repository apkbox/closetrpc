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

    public class SocketClientTransport
    {
        #region Fields

        private readonly string hostname;

        private readonly int port;

        private readonly TcpClient tcpClient = new TcpClient();

        #endregion

        #region Constructors and Destructors

        public SocketClientTransport(string hostname, int port)
        {
            this.hostname = hostname;
            this.port = port;
        }

        #endregion

        #region Public Methods and Operators

        public void Close()
        {
            this.tcpClient.Close();
        }

        public Channel Connect()
        {
            this.tcpClient.Connect(this.hostname, this.port);
            return new Channel(this.tcpClient);
        }

        #endregion
    }
}
