// --------------------------------------------------------------------------------
// <copyright file="IServerTransport.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the IServerTransport type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Net
{
    public interface IServerTransport
    {
        void Cancel();

        Channel Listen();
    }
}
