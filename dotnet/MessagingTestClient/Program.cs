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

    using ClosetRpc;

    using Common.Logging;

    public class Program
    {
        #region Fields

        private readonly ILog log = LogManager.GetLogger<Program>();

        private bool isRunning;

        #endregion

        #region Public Methods and Operators

        public static void Main(string[] args)
        {
            new Program().Run();
        }

        #endregion

        #region Methods

        private void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            this.log.Info("Ctrl+C pressed - shutting down...");
            Console.WriteLine("Ctrl+C pressed - shutting down...");
            this.isRunning = false;
            if (consoleCancelEventArgs.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                consoleCancelEventArgs.Cancel = true;
            }
        }

        private void Run()
        {
            this.log.Info("Application started.");
            Console.WriteLine("Application started.");
            this.isRunning = true;

            Console.CancelKeyPress += this.ConsoleOnCancelKeyPress;

            var transport = new SocketClientTransport("localhost", 3101);

            Client client = null;
            while (this.isRunning)
            {
                try
                {
                    var succeeded = true;
                    this.log.Info("Connecting...");
                    client = new Client(transport);

                    this.log.Info("Connected. Sending messages...");
                    var pingPong = new PingPongProxy(client);

                    for (var i = 0; i < 10000; i++)
                    {
                        try
                        {
                            var result = pingPong.Ping(i);
                            Console.Write("{0}.{1}          \r", i, result);
                        }
                        catch (RpcException ex)
                        {
                            succeeded = false;
                            if (ex.RpcStatus == RpcStatus.ChannelFailure)
                            {
                                this.log.Error("Connection lost.");
                                break;
                            }
                        }
                    }

                    if (succeeded)
                    {
                        this.log.Info("All messages sent. Shutting down...");
                        this.isRunning = false;
                    }
                    else
                    {
                        this.log.Info("Shutting down and retrying after an error.");
                    }

                    client?.Shutdown(false);
                    client = null;
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

            Console.WriteLine("Application exited.");
            this.log.Info("Application exited.");
        }

        #endregion
    }
}
