// --------------------------------------------------------------------------------
// <copyright file="RpcMessage.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcMessage type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Protocol
{
    internal class RpcMessage : IRpcMessage
    {
        #region Public Properties

        public uint RequestId { get; set; }

        #endregion

        #region Explicit Interface Properties

        IRpcCall IRpcMessage.Call => this.Call;

        #endregion

        #region Properties

        internal RpcCall Call { get; set; }

        internal RpcResult Result { get; set; }

        #endregion
    }
}
