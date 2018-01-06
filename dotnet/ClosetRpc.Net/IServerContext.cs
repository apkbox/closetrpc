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

    public interface IServerContext
    {
        #region Public Properties

        IEnumerable<IServerContext> ActiveConnections { get; }

        IEventSource GlobalEventSource { get; }

        IEventSource LocalEventSource { get; }

        ObjectManager ObjectManager { get; }

        object UserData { get; set; }

        #endregion
    }
}
