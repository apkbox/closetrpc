// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessagingTestClient
{
    using System;

    using ClosetRpc.Net;

    public class Program
    {
        #region Public Methods and Operators

        public static void Main(string[] args)
        {
            var transport = new SocketClientTransport("localhost", 3101);
            var client = new Client(transport);

            var pingPong = new PingPongProxy(client);

            for (var i = 0; i < 100000; i++)
            {
                Console.Write("{0}.{1}   ", i, pingPong.Ping(i));
            }

            client.Shutdown();
        }

        #endregion
    }
}
