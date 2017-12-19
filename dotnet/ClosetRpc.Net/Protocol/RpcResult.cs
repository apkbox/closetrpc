// --------------------------------------------------------------------------------
// <copyright file="RpcResult.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcResult type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Protocol
{
    using System.IO;

    internal class RpcResult : IRpcResult
    {
        #region Constructors and Destructors

        public RpcResult()
        {
            this.Status = RpcStatus.Succeeded;
        }

        public RpcResult(BinaryReader reader)
        {
            this.Deserialize(reader);
        }

        #endregion

        #region Public Properties

        public byte[] ResultData { get; set; }

        public RpcStatus Status { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Deserialize(BinaryReader reader)
        {
            this.Status = (RpcStatus)reader.ReadInt32();
            this.ResultData = SerializationHelpers.ReadBytes(reader);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((int)this.Status);
            SerializationHelpers.WriteBytesSafe(writer, this.ResultData);
        }

        #endregion
    }
}
