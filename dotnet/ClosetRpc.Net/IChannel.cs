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

    public interface IChannel
    {
        #region Public Properties

        Stream Stream { get; }

        #endregion

        #region Public Methods and Operators

        void Close();

        #endregion
    }
}
