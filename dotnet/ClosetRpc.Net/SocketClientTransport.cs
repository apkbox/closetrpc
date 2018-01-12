// --------------------------------------------------------------------------------
// <copyright file="SocketClientTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SocketClientTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System.Net.Sockets;

    /// <summary>
    /// The client transport implementation over TCP sockets.
    /// </summary>
    public class SocketClientTransport : IClientTransport
    {
        #region Fields

        private readonly string hostname;

        private readonly int port;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes new instance of the <see cref="SocketClientTransport"/> class.
        /// </summary>
        /// <param name="hostname">TCP/IP host name.</param>
        /// <param name="port">Port number.</param>
        public SocketClientTransport(string hostname, int port)
        {
            this.hostname = hostname;
            this.port = port;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public IChannel Connect()
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(this.hostname, this.port);
            return new Channel(tcpClient);
        }

        #endregion
    }
}
