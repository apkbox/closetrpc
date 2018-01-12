// --------------------------------------------------------------------------------
// <copyright file="IObjectManager.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IObjectManager type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides methods to implement transient object manager.
    /// </summary>
    public interface IObjectManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets an object instance by object ID.
        /// </summary>
        /// <param name="objectId">Object ID.</param>
        /// <returns>Object instance.</returns>
        IRpcServiceStub GetInstance(ulong objectId);

        /// <summary>
        /// Gets service by name.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <returns>Service instance.</returns>
        IRpcServiceStub GetService(string serviceName);

        /// <summary>
        /// Registers an object instance and returns object ID.
        /// </summary>
        /// <param name="instance">Object to register.</param>
        /// <returns>Object ID.</returns>
        ulong RegisterInstance(IRpcServiceStub instance);

        /// <summary>
        /// Registers a service.
        /// </summary>
        /// <param name="service">Service instance.</param>
        void RegisterService(IRpcService service);

        /// <summary>
        /// Registers a service with a custom name.
        /// </summary>
        /// <param name="service">Service instance.</param>
        /// <param name="serviceName">Service name.</param>
        void RegisterService(IRpcServiceStub service, string serviceName);

        #endregion
    }
}
