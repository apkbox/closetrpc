// --------------------------------------------------------------------------------
// <copyright file="RpcCallParameters.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcCallParameters type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Encapsulates parameters required for RPC call.
    /// </summary>
    public class RpcCallParameters
    {
        #region Public Properties

        /// <summary>
        /// Data associated with RPC call requets.
        /// </summary>
        /// <remarks>
        /// Format of calling data is opaque for RPC engine. RPC proxy and stub
        /// implementations are responsible for serializing call parameters.
        /// </remarks>
        public byte[] CallData { get; set; }

        /// <summary>
        /// Gets a value indicating whether the call is asynchronous.
        /// </summary>
        /// <remarks>
        /// Event calls are always asyncrhonous.
        /// </remarks>
        public bool IsAsync { get; set; }

        /// <summary>
        /// Method name.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Transient object ID.
        /// </summary>
        public ulong ObjectId { get; set; }

        /// <summary>
        /// Service interface name.
        /// </summary>
        public string ServiceName { get; set; }

        #endregion
    }
}
