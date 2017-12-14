// --------------------------------------------------------------------------------
// <copyright file="PingPongService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PingPongService type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessagingTestServer
{
    using ClosetRpc.Net;

    internal class PingPongService : IRpcService
    {
        #region Public Properties

        public string Name => "IPingPong";

        #endregion

        #region Public Methods and Operators

        public void CallMethod(ServerContext context, IRpcCall rpcCall, IRpcResult result)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
