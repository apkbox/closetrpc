﻿// --------------------------------------------------------------------------------
// <copyright file="PingPongProxy.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PingPongProxy type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessagingTestClient
{
    using System;

    using ClosetRpc;

    public class PingPongProxy : IPingPong
    {
        #region Fields

        private readonly Client client;

        #endregion

        #region Constructors and Destructors

        public PingPongProxy(Client client)
        {
            this.client = client;
        }

        #endregion

        #region Public Methods and Operators

        public int Ping(int value)
        {
            var call = new RpcCallParameters();
            call.ServiceName = "IPingPong";
            call.MethodName = "Ping";
            call.CallData = BitConverter.GetBytes(value);
            var result = this.client.CallService(call);
            if (result.Status == RpcStatus.ChannelFailure)
            {
                throw new RpcException(result.Status);
            }
            else if (result.Status != RpcStatus.Succeeded)
            {
                throw new ApplicationException();
            }

            return BitConverter.ToInt32(result.ResultData, 0);
        }

        #endregion
    }
}
