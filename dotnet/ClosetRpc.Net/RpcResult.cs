// --------------------------------------------------------------------------------
// <copyright file="RpcResult.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcResult type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    using System.IO;

    internal class RpcResult : IRpcResult
    {
        #region Constructors and Destructors

        public RpcResult(uint requestId)
        {
            this.RequestId = requestId;
            this.Status = RpcStatus.Succeeded;
        }

        public RpcResult(BinaryReader reader)
        {
            this.Deserialize(reader);
        }

        #endregion

        #region Public Properties

        public uint RequestId { get; private set; }

        public byte[] ResultData { get; set; }

        public RpcStatus Status { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Deserialize(BinaryReader reader)
        {
            this.RequestId = reader.ReadUInt32();
            this.Status = (RpcStatus)reader.ReadInt32();

            var length = reader.ReadUInt32();
            this.ResultData = reader.ReadBytes((int)length);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((uint)this.RequestId);
            writer.Write((int)this.Status);

            if (this.ResultData == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                writer.Write((uint)this.ResultData.Length);
                writer.Write(this.ResultData);
            }
        }

        #endregion
    }
}
