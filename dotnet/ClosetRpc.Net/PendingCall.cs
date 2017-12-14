// --------------------------------------------------------------------------------
// <copyright file="PendingCall.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PendingCall type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    internal class PendingCall
    {
        #region Public Properties

        public IRpcResult Result { get; set; }

        public PendingCallStatus Status { get; set; }

        #endregion
    }
}
