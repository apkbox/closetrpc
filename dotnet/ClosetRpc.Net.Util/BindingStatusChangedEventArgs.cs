// --------------------------------------------------------------------------------
// <copyright file="BindingStatusChangedEventArgs.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the BindingEventArgs type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net.Util
{
    using System;

    public class BindingStatusChangedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        public BindingStatusChangedEventArgs(BindingStatus status)
        {
            this.Status = status;
        }

        #endregion

        #region Public Properties

        public BindingStatus Status { get; private set; }

        #endregion
    }
}
