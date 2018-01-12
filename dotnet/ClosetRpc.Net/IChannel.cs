// --------------------------------------------------------------------------------
// <copyright file="IChannel.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IChannel type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc
{
    using System.IO;

    /// <summary>
    /// Provides required functions and properties for RPC communication channels.
    /// </summary>
    public interface IChannel
    {
        #region Public Properties

        /// <summary>
        /// Gets stream uses to send and receive RPC messages.
        /// </summary>
        Stream Stream { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Closes the channel.
        /// </summary>
        void Close();

        #endregion
    }
}
