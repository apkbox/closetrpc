// --------------------------------------------------------------------------------
// <copyright file="ProtocolObjectFactory.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ProtocolObjectFactory type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Protocol
{
    using System.IO;
    using System.Text;

    internal class ProtocolObjectFactory : IProtocolObjectFactory
    {
        #region Public Methods and Operators

        public IRpcCall CreateRpcCall()
        {
            return new RpcCall();
        }

        public IRpcResult CreateRpcResult()
        {
            return new RpcResult();
        }

        public IRpcMessage RpcMessageFromStream(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                var message = new RpcMessage();
                message.RequestId = reader.ReadUInt32();
                message.Call = new RpcCall(reader);
                message.Result = new RpcResult(reader);
                return message;
            }
        }

        public void WriteMessage(Stream stream, uint requestId, IRpcCall call, IRpcResult result)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(requestId);
                this.WriteCall(writer, (RpcCall)call);
                this.WriteResult(writer, (RpcResult)result);
            }
        }

        #endregion

        #region Methods

        private void WriteCall(BinaryWriter writer, RpcCall call)
        {
            if (call == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                call.Serialize(writer);
            }
        }

        private void WriteResult(BinaryWriter writer, RpcResult result)
        {
            if (result == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                result.Serialize(writer);
            }
        }

        #endregion
    }
}
