// --------------------------------------------------------------------------------
// <copyright file="ObjectManager.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ObjectManager type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class ObjectManager : IObjectManager
    {
        #region Fields

        /// <summary>
        /// Contains stub instances and transient objects.
        /// </summary>
        private readonly Dictionary<ulong, IRpcServiceStub> instances = new Dictionary<ulong, IRpcServiceStub>();

        private readonly object lastObjectIdLock = new object();

        /// <summary>
        /// Maps the interface name to a key into <see cref="instances"/> that contains a stub instance.
        /// </summary>
        private readonly Dictionary<string, ulong> services = new Dictionary<string, ulong>();

        private uint lastObjectId;

        #endregion

        #region Public Methods and Operators

        public IRpcServiceStub GetInstance(ulong objectId)
        {
            if (objectId == 0)
            {
                return null;
            }

            if (this.instances.TryGetValue(objectId, out var service))
            {
                return service;
            }

            return null;
        }

        public IRpcServiceStub GetService(string serviceName)
        {
            if (this.services.TryGetValue(serviceName, out var objectId))
            {
                return this.GetInstance(objectId);
            }

            return null;
        }

        public ulong RegisterInstance(IRpcServiceStub instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            ulong objectId;
            lock (this.lastObjectIdLock)
            {
                objectId = ++this.lastObjectId;
            }

            this.instances[objectId] = instance;
            if (this.lastObjectId == 0)
            {
                throw new Exception("Object identifier space seems to run out.");
            }

            return objectId;
        }

        public void RegisterService(IRpcService service)
        {
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

            if (this.services.ContainsKey(serviceName))
            {
                throw new DuplicateNameException("Service with the same name alrady registered.");
            }

            this.services[serviceName] = this.RegisterInstance(service);
        }

        #endregion
    }
}
