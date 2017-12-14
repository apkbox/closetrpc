// --------------------------------------------------------------------------------
// <copyright file="IClientTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IClientTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    public interface IClientTransport
    {
        #region Public Methods and Operators

        Channel Connect();

        #endregion
    }
}
