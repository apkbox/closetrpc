// --------------------------------------------------------------------------------
// <copyright file="IRpcResult.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcResult type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Interface that abstracts method call result.
    /// </summary>
    public interface IRpcResult
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets result data.
        /// </summary>
        byte[] ResultData { get; set; }

        /// <summary>
        /// Gets or sets call status.
        /// </summary>
        RpcStatus Status { get; set; }

        #endregion
    }
}
