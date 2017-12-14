// --------------------------------------------------------------------------------
// <copyright file="RpcCall.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcCall type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
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

        public uint RequestId { get; private set; }

        public bool IsAsync { get; private set; }

        public byte[] CallData { get; private set; }

        public string MethodName { get; private set; }

        public ulong ObjectId { get; private set; }

        public string ServiceName { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Deserialize(BinaryReader reader)
        {
            // TODO: serialize/deserislize flags (for async)
            this.RequestId = reader.ReadUInt32();
            this.ServiceName = reader.ReadString();
            this.MethodName = reader.ReadString();
            this.ObjectId = reader.ReadUInt32();

            var length = reader.ReadUInt32();
            this.CallData = reader.ReadBytes((int)length);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((uint)this.RequestId);

            if (this.ServiceName == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                writer.Write(this.ServiceName);
            }

            if (this.MethodName == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                writer.Write(this.MethodName);
            }

            writer.Write((uint)this.ObjectId);

            if (this.CallData == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                writer.Write((uint)this.CallData.Length);
                writer.Write(this.CallData);
            }
        }

        #endregion
    }
}
