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
    using System.Collections.Concurrent;

    internal class EventServiceManager
    {
        #region Fields

        /// <summary>
        /// Maps the interface name to a handler implementation (in form of stub).
        /// </summary>
        private readonly ConcurrentDictionary<string, IEventHandlerStub> services =
            new ConcurrentDictionary<string, IEventHandlerStub>();

        #endregion

        #region Public Methods and Operators

        public IEventHandlerStub GetHandler(string name)
        {
            IEventHandlerStub handler;
            if (this.services.TryGetValue(name, out handler))
            {
                return handler;
            }

            return null;
        }

        public void RegisterHandler(IEventHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            this.RegisterHandler(handler, handler.Name);
        }

        public void RegisterHandler(IEventHandlerStub eventHandler, string eventServiceName)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException("eventHandler");
            }

            if (eventServiceName == null)
            {
                throw new ArgumentNullException("eventServiceName");
            }

            if (!this.services.TryAdd(eventServiceName, eventHandler))
            {
                throw new ArgumentException("An element with the same key already exists.", "eventServiceName");
            }
        }

        #endregion
    }
}
