// --------------------------------------------------------------------------------
// <copyright file="RpcCall.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcCall type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Protocol
{
    using System.IO;

    internal class RpcCall : IRpcCall
    {
        #region Constructors and Destructors

        public RpcCall()
        {
        }

        public RpcCall(BinaryReader reader)
        {
            this.Deserialize(reader);
        }

        public RpcCall(RpcCallBuilder callBuilder)
        {
            this.IsAsync = callBuilder.IsAsync;
            this.ServiceName = callBuilder.ServiceName;
            this.MethodName = callBuilder.MethodName;
            this.ObjectId = callBuilder.ObjectId;
            this.CallData = callBuilder.CallData;
        }

        #endregion

        #region Public Properties

        public byte[] CallData { get; private set; }

        public bool IsAsync { get; private set; }

        public string MethodName { get; private set; }

        public ulong ObjectId { get; private set; }

        public string ServiceName { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Deserialize(BinaryReader reader)
        {
            this.IsAsync = (reader.ReadInt32() & 1) != 0;
            this.ServiceName = reader.ReadString();
            this.MethodName = reader.ReadString();
            this.ObjectId = reader.ReadUInt32();
            this.CallData = SerializationHelpers.ReadBytes(reader);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((uint)(this.IsAsync ? 1 : 0));
            SerializationHelpers.WriteStringSafe(writer, this.ServiceName);
            SerializationHelpers.WriteStringSafe(writer, this.MethodName);
            writer.Write((uint)this.ObjectId);
            SerializationHelpers.WriteBytesSafe(writer, this.CallData);
        }

        #endregion
    }
}
