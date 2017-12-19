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
    internal class LocalEventSource : IEventSource
    {
        private Server server;
        private Channel channel;

        public LocalEventSource(Server server, Channel channel)
        {
            this.server = server;
            this.channel = channel;
        }

        public void SendEvent(IRpcCallBuilder rpcCall)
        {
            this.server.SendEvent(rpcCall, channel);
        }
    }
}
