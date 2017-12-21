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
    public class RpcCallParameters
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
