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

        #endregion

        #region Public Properties

        public byte[] CallData { get; private set; }

        public bool IsAsync { get; private set; }

        public string MethodName { get; private set; }

        public ulong ObjectId { get; private set; }

        public uint RequestId { get; private set; }

        public string ServiceName { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Deserialize(BinaryReader reader)
        {
            this.RequestId = reader.ReadUInt32();
            this.ServiceName = reader.ReadString();
            this.MethodName = reader.ReadString();
            this.IsAsync = (reader.ReadInt32() & 1) != 0;
            this.ObjectId = reader.ReadUInt32();
            this.CallData = SerializationHelpers.ReadBytes(reader);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((uint)this.RequestId);
            SerializationHelpers.WriteStringSafe(writer, this.ServiceName);
            SerializationHelpers.WriteStringSafe(writer, this.MethodName);
            writer.Write((uint)(this.IsAsync ? 1 : 0));
            writer.Write((uint)this.ObjectId);
            SerializationHelpers.WriteBytesSafe(writer, this.CallData);
        }

        #endregion
    }
}
