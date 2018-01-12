// --------------------------------------------------------------------------------
// <copyright file="IEventHandler.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IEventHandler type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides interface name for an event handler.
    /// </summary>
    public interface IEventHandler : IEventHandlerStub
    {
        #region Public Properties

        /// <summary>
        /// Gets an event interface name.
        /// </summary>
        string Name { get; }

        #endregion
    }
}
