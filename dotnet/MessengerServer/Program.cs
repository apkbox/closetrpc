// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    using System;

    using ClosetRpc;

    using Common.Logging;

    class Program
    {
        #region Fields

        private readonly ILog log = LogManager.GetLogger<Program>();

        private RpcServer server;

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
            this.server.Shutdown(true);
            if (consoleCancelEventArgs.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                consoleCancelEventArgs.Cancel = true;
            }
        }

        private void Run()
        {
            this.log.Info("Application started.");
            Console.WriteLine("Application started.");
            Console.CancelKeyPress += this.ConsoleOnCancelKeyPress;

            this.server = new RpcServer(new SocketServerTransport(8020));
            var bl = new BusinessLogic();
            this.server.RegisterService(new LoginService(bl));
            this.server.RegisterService(new ContactListService(bl));
            this.server.RegisterService(new MessageService(bl));
            this.server.Run();

            Console.WriteLine("Application exited.");
            this.log.Info("Application exited.");
        }

        #endregion
    }
}
