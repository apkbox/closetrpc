// --------------------------------------------------------------------------------
// <copyright file="IClientTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IClientTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides required functions to create a client side channel.
    /// </summary>
    public interface IClientTransport
    {
        #region Public Methods and Operators

        /// <summary>
        /// Establishes connection with remote peer.
        /// </summary>
        /// <returns>Communication channel instance.</returns>
        IChannel Connect();

        #endregion
    }
}
