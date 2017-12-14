// --------------------------------------------------------------------------------
// <copyright file="SocketServerTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SocketServerTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System.Net;
    using System.Net.Sockets;

    public class SocketServerTransport : IServerTransport
    {
        #region Fields

        private readonly TcpListener listener;

        #endregion

        #region Constructors and Destructors

        public SocketServerTransport(int portNumber)
        {
            this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, portNumber));
        }

        #endregion

        #region Public Methods and Operators

        public void Cancel()
        {
            this.listener.Stop();
        }

        public Channel Listen()
        {
            this.listener.Start();
            var client = this.listener.AcceptTcpClient();
            this.listener.Stop();
            return new Channel(client);
        }

        #endregion
    }
}
