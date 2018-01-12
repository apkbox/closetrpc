// --------------------------------------------------------------------------------
// <copyright file="IProtocolObjectFactory.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IProtocolObjectFactory type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System.IO;

    /// <summary>
    /// Provides methods to create RPC command protocol objects.
    /// </summary>
    /// <remarks>
    /// Protocol objects provide a way to serialize and deserialize RPC protocol
    /// messages from the stream.
    /// </remarks>
    public interface IProtocolObjectFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates a call object that includes call parameters.
        /// </summary>
        /// <param name="callParameters">Call parameters.</param>
        /// <returns>Object that encapsulates call parameters.</returns>
        IRpcCall BuildCall(RpcCallParameters callParameters);

        /// <summary>
        /// Creates and RPC result object used to return values from RPC calls.
        /// </summary>
        /// <returns>RPC result object.</returns>
        IRpcResult CreateRpcResult();

        /// <summary>
        /// Parses and returns RPC message from the stream.
        /// </summary>
        /// <param name="stream">RPC channel stream.</param>
        /// <returns></returns>
        IRpcMessage RpcMessageFromStream(Stream stream);

        /// <summary>
        /// Writes a call and/or result parameters into the RPC stream.
        /// </summary>
        /// <param name="stream">RPC channel stream.</param>
        /// <param name="requestId">Unique request identifier. This parameter must be passed as part of the message.</param>
        /// <param name="call">Call data. This parameter can be null.</param>
        /// <param name="result">Result data. This parameter can be null.</param>
        void WriteMessage(Stream stream, uint requestId, IRpcCall call, IRpcResult result);

        #endregion
    }
}
