// --------------------------------------------------------------------------------
// <copyright file="SocketServerTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SocketServerTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// The server transport implementation over TCP sockets.
    /// </summary>
    public class SocketServerTransport : IServerTransport
    {
        #region Fields

        private readonly TcpListener listener;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes new instance of the <see cref="SocketServerTransport"/> class.
        /// </summary>
        /// <param name="portNumber">Port number.</param>
        public SocketServerTransport(int portNumber)
        {
            this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, portNumber));
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public void Cancel()
        {
            this.listener.Stop();
        }

        /// <inheritdoc />
        public IChannel Listen()
        {
            this.listener.Start();
            var client = this.listener.AcceptTcpClient();
            this.listener.Stop();
            return new Channel(client);
        }

        #endregion
    }
}
