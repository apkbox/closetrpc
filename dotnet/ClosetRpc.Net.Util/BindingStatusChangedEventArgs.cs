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

    /// <summary>
    /// Provides binding status arguments.
    /// </summary>
    public class BindingStatusChangedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes <see cref="BindingStatusChangedEventArgs"/> class.
        /// </summary>
        public BindingStatusChangedEventArgs(bool bound)
        {
            this.IsBound = bound;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets binding status.
        /// </summary>
        public bool IsBound { get; private set; }

        #endregion
    }
}
