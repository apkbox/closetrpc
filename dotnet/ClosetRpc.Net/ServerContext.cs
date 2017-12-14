// --------------------------------------------------------------------------------
// <copyright file="ServerContext.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ServerContext type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System.Threading;

    // TODO: Extract interface
    public class ServerContext
    {
        #region Constructors and Destructors

        public ServerContext(Channel channel, Thread thread)
        {
            this.Channel = channel;
            this.ObjectManager = new ObjectManager();
        }

        #endregion

        #region Public Properties

        public Channel Channel { get; set; }

        public ObjectManager ObjectManager { get; private set; }

        public Thread Thread { get; set; }

        #endregion
    }
}
