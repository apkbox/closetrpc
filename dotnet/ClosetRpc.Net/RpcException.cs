// --------------------------------------------------------------------------------
// <copyright file="RpcException.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcException type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;

    /// <summary>
    /// Represents errors that occur during RPC call.
    /// </summary>
    public class RpcException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes new instance of the <see cref="RpcException"/> class.
        /// </summary>
        public RpcException()
        {
        }

        /// <summary>
        /// Initializes new instance of the <see cref="RpcException"/> class.
        /// </summary>
        /// <param name="reason">Error reason.</param>
        public RpcException(RpcStatus reason)
        {
            this.RpcStatus = reason;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets exception reason.
        /// </summary>
        public RpcStatus RpcStatus { get; set; }

        #endregion
    }
}
