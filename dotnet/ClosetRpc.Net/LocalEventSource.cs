// --------------------------------------------------------------------------------
// <copyright file="LocalEventSource.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the LocalEventSource type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    internal class LocalEventSource : IEventSource
    {
        #region Fields

        private readonly IChannel channel;

        private readonly Server server;

        #endregion

        #region Constructors and Destructors

        public LocalEventSource(Server server, IChannel channel)
        {
            this.server = server;
            this.channel = channel;
        }

        #endregion

        #region Public Methods and Operators

        public void SendEvent(RpcCallParameters rpcCall)
        {
            this.server.SendEvent(rpcCall, this.channel);
        }

        #endregion
    }
}
