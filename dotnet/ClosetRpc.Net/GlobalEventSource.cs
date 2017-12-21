﻿// --------------------------------------------------------------------------------
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

        private readonly Server server;

        #endregion

        #region Constructors and Destructors

        internal GlobalEventSource(Server server)
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
