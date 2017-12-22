// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------

namespace ProtobufTestApp
{
    using System;
    using System.Threading;

    using ClosetRpc;

    using ProtobufTestApp.Services;

    public class Program
    {
        #region Static Fields

        private static Server serverInstance;

        #endregion

        #region Methods

        private static void ClientThread()
        {
            var transport = new SocketClientTransport("localhost", 4242);
            var client = new Client(transport);
            var eventHandler = new SettingsEvents_Handler();
            client.AddEventListener(eventHandler);
            eventHandler.Changed += Program.EventHandlerOnChanged;
            var settings = new SettingsService_Proxy(client);

            settings.Set(
                new SettingList
                    {
                        Item =
                            {
                                new Setting() { Key = "Setting 1", Value = "Value one" },
                                new Setting() { Key = "Setting 2", Value = "Value two" },
                                new Setting() { Key = "Setting 3", Value = "Value three" }
                            }
                    });

            var settingsList = settings.Get(new SettingKeyList { Item = { "Setting 1", "Setting 2" } });

            foreach (var setting in settingsList.Item)
            {
                Console.WriteLine("{0} = {1}", setting.Key, setting.Value);
            }

            for (var i = 0; i < 10; i++)
            {
                if (!client.PumpEvents())
                {
                    client.WaitForEvents(500);
                }
            }

            client.Shutdown(false);
        }

        private static void EventHandlerOnChanged(object sender, SettingList settingList)
        {
            Console.WriteLine("Settings changed!");
        }

        static void Main(string[] args)
        {
            var serverThread = new Thread(Program.ServerThread);
            var clientThread = new Thread(Program.ClientThread);

            serverThread.Start();
            clientThread.Start();

            clientThread.Join();

            Program.serverInstance?.Shutdown(true);
            serverThread.Join();
        }

        private static void ServerThread()
        {
            var transport = new SocketServerTransport(4242);
            var server = new Server(transport);
            Program.serverInstance = server;
            server.RegisterService(new SettingsService());
            server.Run();
        }

        #endregion
    }
}
