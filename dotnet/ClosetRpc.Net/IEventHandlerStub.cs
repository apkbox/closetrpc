// --------------------------------------------------------------------------------
// <copyright file="IEventHandlerStub.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IEventHandlerStub type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides interface for RPC client to call when event message is received.
    /// </summary>
    public interface IEventHandlerStub
    {
        #region Public Methods and Operators

        /// <summary>
        /// Parses event message and calls client event handler function.
        /// </summary>
        /// <param name="rpcCall"></param>
        void CallMethod(IRpcCall rpcCall);

        #endregion
    }
}
