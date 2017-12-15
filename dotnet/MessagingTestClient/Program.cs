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
    using System.Net.Sockets;
    using System.Threading;

    using ClosetRpc.Net;

    public class Program
    {
        #region Static Fields

        private static bool isRunning;

        #endregion

        #region Public Methods and Operators

        public static void Main(string[] args)
        {
            Program.isRunning = true;
            Console.CancelKeyPress += Program.ConsoleOnCancelKeyPress;
            var transport = new SocketClientTransport("localhost", 3101);
            Client client = null;
            while (Program.isRunning)
            {
                try
                {
                    Console.WriteLine("Trying to connect...");
                    client = new Client(transport);
                    var pingPong = new PingPongProxy(client);

                    for (var i = 0; i < 100000; i++)
                    {
                        Console.Write("{0}.{1}   ", i, pingPong.Ping(i));
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                }
            }

            client?.Shutdown();
        }

        #endregion

        #region Methods

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            Program.isRunning = false;
        }

        #endregion
    }
}
