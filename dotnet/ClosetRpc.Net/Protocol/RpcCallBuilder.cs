// --------------------------------------------------------------------------------
// <copyright file="RpcCallBuilder.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcCallBuilder type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Protocol
{
    internal class RpcCallBuilder : IRpcCallBuilder
    {
        #region Public Properties

        public byte[] CallData { get; set; }

        public bool IsAsync { get; set; }

        public string MethodName { get; set; }

        public ulong ObjectId { get; set; }

        public string ServiceName { get; set; }

        #endregion
    }
}
