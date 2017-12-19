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
    internal class GlobalEventSource : IEventSource
    {
        private readonly Server server;

        internal GlobalEventSource(Server server)
        {
            this.server = server;
        }

        public void SendEvent(IRpcCallBuilder rpcCall)
        {
            server.SendEvent(rpcCall, null);
        }
    }
}
