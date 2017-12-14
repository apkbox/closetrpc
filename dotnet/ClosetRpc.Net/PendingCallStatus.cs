// --------------------------------------------------------------------------------
// <copyright file="PendingCallStatus.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PendingCallStatus type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    internal enum PendingCallStatus
    {
        AwaitingResult,

        Received,

        Cancelled
    }
}
