// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessagingTestServer
{
    using System;

    class Program
    {
        private static PingPongServer server;

        #region Methods

        static void Main(string[] args)
        {
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;
            Program.server = new PingPongServer();
            server.Run();
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            Program.server.Stop();
        }

        #endregion
    }
}
