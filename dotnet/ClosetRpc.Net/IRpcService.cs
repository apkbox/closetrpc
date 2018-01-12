// --------------------------------------------------------------------------------
// <copyright file="IRpcService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcService type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides interface name for an service call handler.
    /// </summary>
    public interface IRpcService : IRpcServiceStub
    {
        #region Public Properties

        /// <summary>
        /// Gets the service interface name.
        /// </summary>
        string Name { get; }

        #endregion
    }
}
