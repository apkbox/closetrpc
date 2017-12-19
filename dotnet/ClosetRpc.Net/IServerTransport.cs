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
    public interface IServerTransport
    {
        void Cancel();

        IChannel Listen();
    }
}
