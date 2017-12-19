// --------------------------------------------------------------------------------
// <copyright file="PendingCall.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PendingCall type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    internal class PendingCall
    {
        #region Constructors and Destructors

        public PendingCall()
        {
            this.Status = PendingCallStatus.AwaitingResult;
        }

        #endregion

        #region Public Properties

        public IRpcResult Result { get; set; }

        public PendingCallStatus Status { get; set; }

        #endregion
    }
}
