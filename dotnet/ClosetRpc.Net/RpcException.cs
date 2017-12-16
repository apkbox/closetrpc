// --------------------------------------------------------------------------------
// <copyright file="RpcException.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcException type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System;

    public class RpcException : Exception
    {
        #region Constructors and Destructors

        public RpcException()
        {
        }

        public RpcException(RpcStatus reason)
        {
            this.RpcStatus = reason;
        }

        #endregion

        #region Public Properties

        public RpcStatus RpcStatus { get; set; }

        #endregion
    }
}
