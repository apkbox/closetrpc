// --------------------------------------------------------------------------------
// <copyright file="ProtocolObjectFactory.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ProtocolObjectFactory type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Protocol
{
    using System.IO;
    using System.Text;

    internal class ProtocolObjectFactory : IProtocolObjectFactory
    {
        #region Constants

        private const byte HasCallFlag = 0x01;

        private const byte HasResultFlag = 0x02;

        #endregion

        #region Public Methods and Operators

        public IRpcCall BuildCall(RpcCallParameters callParameters)
        {
            return new RpcCall(callParameters);
        }

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
                var contentFlags = reader.ReadByte();
                if ((contentFlags & ProtocolObjectFactory.HasCallFlag) != 0)
                {
                    message.Call = new RpcCall(reader);
                }

                if ((contentFlags & ProtocolObjectFactory.HasResultFlag) != 0)
                {
                    message.Result = new RpcResult(reader);
                }

                return message;
            }
        }

        public void WriteMessage(Stream stream, uint requestId, IRpcCall call, IRpcResult result)
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(requestId);

                byte contentFlags = 0;
                contentFlags |= call != null ? ProtocolObjectFactory.HasCallFlag : (byte)0;
                contentFlags |= result != null ? ProtocolObjectFactory.HasResultFlag : (byte)0;
                writer.Write(contentFlags);

                if (call != null)
                {
                    ((RpcCall)call).Serialize(writer);
                }

                if (result != null)
                {
                    ((RpcResult)result).Serialize(writer);
                }
            }
        }

        #endregion
    }
}
