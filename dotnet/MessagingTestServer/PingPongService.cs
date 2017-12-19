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
    using System;

    using ClosetRpc;

    internal class PingPongService : IRpcService
    {
        #region Public Properties

        public string Name => "IPingPong";

        #endregion

        #region Public Methods and Operators

        public void CallMethod(IServerContext context, IRpcCall rpcCall, IRpcResult result)
        {
            if (rpcCall.MethodName == "Ping")
            {
                if (rpcCall.CallData == null || rpcCall.CallData.Length != 4)
                {
                    result.Status = RpcStatus.InvalidCallParameter;
                    return;
                }

                var counter = BitConverter.ToInt32(rpcCall.CallData, 0);
                ++counter;
                result.ResultData = BitConverter.GetBytes(counter);
            }
            else
            {
                result.Status = RpcStatus.UnknownMethod;
            }
        }

        #endregion
    }
}
