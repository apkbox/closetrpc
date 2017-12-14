// --------------------------------------------------------------------------------
// <copyright file="IProtocolObjectFactory.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IProtocolObjectFactory type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System.IO;

    public interface IProtocolObjectFactory
    {
        #region Public Methods and Operators

        IRpcCall RpcCallFromStream(Stream stream);

        IRpcResult RpcResultFromStream(Stream stream);

        void WriteToStream(IRpcCall call, Stream stream);

        void WriteToStream(IRpcResult result, Stream stream);

        #endregion
    }
}
