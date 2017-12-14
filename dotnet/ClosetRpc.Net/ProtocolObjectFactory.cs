// --------------------------------------------------------------------------------
// <copyright file="ProtocolObjectFactory.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ProtocolObjectFactory type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System.IO;
    using System.Text;

    class ProtocolObjectFactory : IProtocolObjectFactory
    {
        public IRpcCall RpcCallFromStream(Stream stream)
        {
            throw new System.NotImplementedException();
        }

        public IRpcResult RpcResultFromStream(Stream stream)
        {
            throw new System.NotImplementedException();
        }

        public void WriteToStream(IRpcCall call, Stream stream)
        {
        }

        public void WriteToStream(IRpcResult result, Stream stream)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {

            }
        }
    }
}
