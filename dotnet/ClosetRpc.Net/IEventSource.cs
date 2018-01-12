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
    /// <summary>
    /// Provides a method to send events.
    /// </summary>
    public interface IEventSource
    {
        #region Public Methods and Operators

        /// <summary>
        /// Sends an event.
        /// </summary>
        /// <param name="rpcCall">Event parameters.</param>
        void SendEvent(RpcCallParameters rpcCall);

        #endregion
    }
}
