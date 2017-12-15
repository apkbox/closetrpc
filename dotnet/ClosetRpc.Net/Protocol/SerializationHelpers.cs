// --------------------------------------------------------------------------------
// <copyright file="SerializationHelpers.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SerializationHelpers type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Protocol
{
    using System.IO;

    internal static class SerializationHelpers
    {
        #region Public Methods and Operators

        public static byte[] ReadBytes(BinaryReader reader)
        {
            var length = reader.ReadUInt32();
            return reader.ReadBytes((int)length);
        }

        public static void WriteBytesSafe(BinaryWriter writer, byte[] bytes)
        {
            if (bytes == null)
            {
                writer.Write((uint)0);
            }
            else
            {
                writer.Write((uint)bytes.Length);
                writer.Write(bytes);
            }
        }

        public static void WriteStringSafe(BinaryWriter writer, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                writer.Write((byte)0);
            }
            else
            {
                writer.Write(str);
            }
        }

        #endregion
    }
}
