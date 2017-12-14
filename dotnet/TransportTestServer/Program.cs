// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------

namespace TransportTestServer
{
    using System;
    using System.Threading;

    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            var server = new PingPongServer();
            var thread = new Thread(server.Run);
            thread.Start();
            Console.WriteLine("Press 'q' and enter to exit...");
            Console.ReadLine();
            server.Stop();
            thread.Join();
        }

        #endregion
    }
}
