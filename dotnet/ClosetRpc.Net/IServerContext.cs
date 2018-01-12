// --------------------------------------------------------------------------------
// <copyright file="IServerContext.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IServerContext type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System.Collections.Generic;

    /// <summary>
    /// Privides context of service call.
    /// </summary>
    /// <remarks>
    /// Context instance is passed as a parameter a service stub and should not
    /// be saved.
    /// </remarks>
    public interface IServerContext
    {
        #region Public Properties

        /// <summary>
        /// Gets snapshot of active connection contexts.
        /// </summary>
        /// <remarks>
        /// The returned contexts can be used to obtain local event source
        /// to send events to a client or clients unrelated to the current
        /// connection.
        /// </remarks>
        IEnumerable<IServerContext> ActiveConnections { get; }

        /// <summary>
        /// Gets an event source that allows to send events to all connected
        /// clients.
        /// </summary>
        IEventSource GlobalEventSource { get; }

        /// <summary>
        /// Gets an event source that allows to send events to the client associcated with
        /// the curren connection only.
        /// </summary>
        IEventSource LocalEventSource { get; }

        /// <summary>
        /// Object manager is used to register transient objects and obtain object ID.
        /// </summary>
        IObjectManager ObjectManager { get; }

        /// <summary>
        /// Custom data associated with the connection.
        /// </summary>
        object UserData { get; set; }

        #endregion
    }
}
