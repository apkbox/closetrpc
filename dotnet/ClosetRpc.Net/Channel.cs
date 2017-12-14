// --------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Channel type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System;
    using System.IO;
    using System.Net.Sockets;

    public class Channel : IDisposable
    {
        #region Fields

        private readonly TcpClient tcpClient;

        #endregion

        #region Constructors and Destructors

        internal Channel(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        #endregion

        #region Public Properties

        public Stream Stream => this.tcpClient.GetStream();

        #endregion

        #region Public Methods and Operators

        public void Close()
        {
            this.tcpClient.Close();
        }

        #endregion

        public void Dispose()
        {
            this.tcpClient?.Dispose();
        }
    }
} // namespace ClosetRpc.Net
