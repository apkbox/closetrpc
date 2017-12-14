// --------------------------------------------------------------------------------
// <copyright file="IRpcMessage.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcMessage type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    public interface IRpcMessage
    {
        #region Public Properties

        IRpcCall Call { get; }

        uint RequestId { get; }

        #endregion
    }
}
