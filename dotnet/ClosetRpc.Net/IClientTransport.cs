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
    public interface IClientTransport
    {
        #region Public Methods and Operators

        IChannel Connect();

        #endregion
    }
}
