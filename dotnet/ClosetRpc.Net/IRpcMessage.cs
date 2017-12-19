// --------------------------------------------------------------------------------
// <copyright file="IRpcMessage.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcMessage type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    public interface IRpcMessage
    {
        #region Public Properties

        IRpcCall Call { get; }

        uint RequestId { get; }

        IRpcResult Result { get; }

        #endregion
    }
}
