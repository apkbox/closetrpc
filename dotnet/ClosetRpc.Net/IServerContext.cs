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
    public interface IServerContext
    {
        #region Public Properties

        IEventSource GlobalEventSource { get; }

        IEventSource LocalEventSource { get; }

        ObjectManager ObjectManager { get; }

        #endregion
    }
}
