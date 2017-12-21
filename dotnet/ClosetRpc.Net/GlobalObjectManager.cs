﻿// --------------------------------------------------------------------------------
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
    /// TODO: Make thread safe
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

            this.services.Add(serviceName, service);
        }

        #endregion
    }
}
