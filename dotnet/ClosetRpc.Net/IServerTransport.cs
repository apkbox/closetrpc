// --------------------------------------------------------------------------------
// <copyright file="IServerTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IServerTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    /// <summary>
    /// Provides required functions to create a server side channel.
    /// </summary>
    public interface IServerTransport
    {
        /// <summary>
        /// Cancels pending connection requests.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Synchronously listen for incoming connections.
        /// </summary>
        /// <returns></returns>
        IChannel Listen();
    }
}
