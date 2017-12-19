// --------------------------------------------------------------------------------
// <copyright file="Channel.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Channel type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    using Common.Logging;

    internal class Channel : IDisposable, IChannel
    {
        #region Static Fields

        private static int lastChannelId;

        #endregion

        #region Fields

        private readonly int channelId;

        private readonly ILog log = LogManager.GetLogger<Channel>();

        private readonly TcpClient tcpClient;

        #endregion

        #region Constructors and Destructors

        internal Channel(TcpClient tcpClient)
        {
            this.channelId = Interlocked.Increment(ref Channel.lastChannelId);
            this.log.DebugFormat("Created channel {0}", this.channelId);
            this.tcpClient = tcpClient;
        }

        #endregion

        #region Public Properties

        public Stream Stream
        {
            get
            {
                return this.tcpClient.GetStream();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Close()
        {
            this.log.DebugFormat("Closing channel {0}", this.channelId);
            this.tcpClient.Close();
        }

        public void Dispose()
        {
            this.log.DebugFormat("Disposing channel {0}", this.channelId);
            this.tcpClient?.Dispose();
        }

        #endregion
    }
}
