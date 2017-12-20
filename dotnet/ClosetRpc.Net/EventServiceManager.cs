// --------------------------------------------------------------------------------
// <copyright file="EventServiceManager.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the EventServiceManager type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;
    using System.Collections.Generic;

    internal class EventServiceManager
    {
        #region Fields

        /// <summary>
        /// Maps the interface name to a handler implementation (in form of stub).
        /// </summary>
        private readonly Dictionary<string, IEventHandlerStub> services = new Dictionary<string, IEventHandlerStub>();

        #endregion

        #region Public Methods and Operators

        public IEventHandlerStub GetHandler(string name)
        {
            if (this.services.TryGetValue(name, out var handler))
            {
                return handler;
            }

            return null;
        }

        public void RegisterHandler(IEventHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            this.RegisterHandler(handler, handler.Name);
        }

        public void RegisterHandler(IEventHandlerStub eventHandler, string eventServiceName)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            if (eventServiceName == null)
            {
                throw new ArgumentNullException(nameof(eventServiceName));
            }

            this.services.Add(eventServiceName, eventHandler);
        }

        #endregion
    }
}
