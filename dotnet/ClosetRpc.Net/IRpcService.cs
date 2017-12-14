// --------------------------------------------------------------------------------
// <copyright file="IRpcService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IRpcService type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    public interface IRpcService : IRpcServiceStub
    {
        #region Public Properties

        /// <summary>
        /// Gets the service name.
        /// </summary>
        string Name { get; }

        #endregion
    }
}
