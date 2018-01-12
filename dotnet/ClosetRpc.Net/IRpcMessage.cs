// --------------------------------------------------------------------------------
// <copyright file="IRpcMessage.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcMessage type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Encapsulates an RPC message received from remote peer.
    /// </summary>
    public interface IRpcMessage
    {
        #region Public Properties

        /// <summary>
        /// RPC call data.
        /// </summary>
        /// <remarks>
        /// Call data in received message used for event invocation.
        /// </remarks>
        IRpcCall Call { get; }

        /// <summary>
        /// RPC request ID that allows to match requests with responses.
        /// </summary>
        uint RequestId { get; }

        /// <summary>
        /// RPC result data.
        /// </summary>
        IRpcResult Result { get; }

        #endregion
    }
}
