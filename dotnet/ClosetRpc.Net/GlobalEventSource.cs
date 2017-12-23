// --------------------------------------------------------------------------------
// <copyright file="GlobalEventSource.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the GlobalEventSource type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    internal class GlobalEventSource : IEventSource
    {
        #region Fields

        private readonly RpcServer server;

        #endregion

        #region Constructors and Destructors

        internal GlobalEventSource(RpcServer server)
        {
            this.server = server;
        }

        #endregion

        #region Public Methods and Operators

        public void SendEvent(RpcCallParameters rpcCall)
        {
            this.server.SendEvent(rpcCall, null);
        }

        #endregion
    }
}
