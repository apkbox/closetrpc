// --------------------------------------------------------------------------------
// <copyright file="GlobalObjectManager.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the GlobalObjectManager type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Manages services registered with the service.
    /// </summary>
    internal class GlobalObjectManager
    {
        #region Fields

        /// <summary>
        /// Maps the interface name to a service implementation (in form of stub).
        /// </summary>
        private readonly ConcurrentDictionary<string, IRpcServiceStub> services =
            new ConcurrentDictionary<string, IRpcServiceStub>();

        #endregion

        #region Public Methods and Operators

        public IRpcServiceStub GetService(string name)
        {
            IRpcServiceStub service;
            if (this.services.TryGetValue(name, out service))
            {
                return service;
            }

            return null;
        }

        public void RegisterService(IRpcService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.RegisterService(service, service.Name);
        }

        public void RegisterService(IRpcServiceStub service, string serviceName)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            if (serviceName == null)
            {
                throw new ArgumentNullException("serviceName");
            }

            if (!this.services.TryAdd(serviceName, service))
            {
                throw new ArgumentException("An element with the same key already exists.", "serviceName");
            }
        }

        #endregion
    }
}
