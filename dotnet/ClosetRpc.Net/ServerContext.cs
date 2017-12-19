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

        public ServerContext(Server server, Channel channel, Thread thread)
        {
            this.Server = server;
            this.Channel = channel;
            this.Thread = thread;
            this.ObjectManager = new ObjectManager();
            this.GlobalEventSource = new GlobalEventSource(server);
            this.LocalEventSource = new LocalEventSource(server, channel);
        }

        public Server Server { get; }

        #endregion

        #region Public Properties

        public Channel Channel { get; set; }

        public ObjectManager ObjectManager { get; }

        public Thread Thread { get; set; }

        public IEventSource GlobalEventSource { get; }

        public IEventSource LocalEventSource { get; }

        #endregion
    }
}
