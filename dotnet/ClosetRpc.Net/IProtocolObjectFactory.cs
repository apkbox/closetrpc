﻿// --------------------------------------------------------------------------------
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

    public interface IProtocolObjectFactory
    {
        #region Public Methods and Operators

        IRpcCall BuildCall(IRpcCallBuilder callBuilder);

        IRpcCallBuilder CreateCallBuilder();

        IRpcResult CreateRpcResult();

        IRpcMessage RpcMessageFromStream(Stream stream);

        void WriteMessage(Stream stream, uint requestId, IRpcCall call, IRpcResult result);

        #endregion
    }
}
