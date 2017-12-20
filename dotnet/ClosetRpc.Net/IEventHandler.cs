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
    public interface IEventHandler : IEventHandlerStub
    {
        #region Public Properties

        string Name { get; }

        #endregion
    }
}
