// --------------------------------------------------------------------------------
// <copyright file="IEventSource.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IEventSource type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    public interface IEventSource
    {
        #region Public Methods and Operators

        void SendEvent(IRpcCallBuilder rpcCall);

        #endregion
    }
}
