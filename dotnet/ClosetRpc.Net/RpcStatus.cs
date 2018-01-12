// --------------------------------------------------------------------------------
// <copyright file="RpcStatus.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcStatus type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Status of RPC call.
    /// </summary>
    public enum RpcStatus
    {
        /// <summary>
        /// Call succeeded.
        /// </summary>
        Succeeded = 0,

        /// <summary>
        /// Channel failure.
        /// </summary>
        ChannelFailure = 1,

        /// <summary>
        /// Requested method not found.
        /// </summary>
        UnknownMethod = 2,

        /// <summary>
        /// Protocol error.
        /// </summary>
        ProtocolError = 3,

        /// <summary>
        /// Requested interface not found.
        /// </summary>
        UnknownInterface = 4,

        /// <summary>
        /// Invalid call parameter.
        /// </summary>
        InvalidCallParameter = 5,

        /// <summary>
        /// Internal error.
        /// </summary>
        InternalError = 6
    }
}
