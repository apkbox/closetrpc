// --------------------------------------------------------------------------------
// <copyright file="ServerContext.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ServerContext type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System.Threading;

    public class ServerContext : IServerContext
    {
        #region Constructors and Destructors

        public ServerContext(RpcServer server, IChannel channel, Thread thread)
        {
            this.Server = server;
            this.Channel = channel;
            this.Thread = thread;
            this.ObjectManager = new ObjectManager();
            this.GlobalEventSource = server.EventSource;
            this.LocalEventSource = new LocalEventSource(server, channel);
        }

        #endregion

        #region Public Properties

        public IChannel Channel { get; set; }

        public IEventSource GlobalEventSource { get; private set; }

        public IEventSource LocalEventSource { get; private set; }

        public ObjectManager ObjectManager { get; private set; }

        public RpcServer Server { get; private set; }

        public Thread Thread { get; set; }

        #endregion
    }
}
