// --------------------------------------------------------------------------------
// <copyright file="IRpcServiceStub.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcServiceStub type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides interface for RPC server to call when call message is received.
    /// </summary>
    public interface IRpcServiceStub
    {
        #region Public Methods and Operators

        /// <summary>
        /// Parses the RPC call message, executes the method and updates RPC result message.
        /// </summary>
        /// <param name="context">Server context.</param>
        /// <param name="rpcCall">RPC call message.</param>
        /// <param name="rpcResult">RPC result message.</param>
        void CallMethod(IServerContext context, IRpcCall rpcCall, IRpcResult rpcResult);

        #endregion
    }
}
