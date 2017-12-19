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
    using System.Collections.Generic;

    /// <summary>
    /// Manages services registered with the service.
    /// </summary>
    internal class GlobalObjectManager
    {
        #region Fields

        /// <summary>
        /// Maps the interface name to a service implementation (in form of stub).
        /// </summary>
        private readonly Dictionary<string, IRpcServiceStub> services = new Dictionary<string, IRpcServiceStub>();

        #endregion

        #region Public Methods and Operators

        public IRpcServiceStub GetService(string name)
        {
            if (this.services.TryGetValue(name, out var service))
            {
                return service;
            }

            return null;
        }

        public void RegisterService(IRpcService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.RegisterService(service, service.Name);
        }

        public void RegisterService(IRpcServiceStub service, string serviceName)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (serviceName == null)
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            this.services.Add(serviceName, service);
        }

        #endregion
    }
}
