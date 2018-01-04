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
        #region Fields

        private BindingStatus status;

        #endregion

        #region Constructors and Destructors

        public BindingStatusChangedEventArgs(BindingStatus status)
        {
            this.status = status;
        }

        #endregion
    }
}
