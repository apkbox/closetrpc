// --------------------------------------------------------------------------------
// <copyright file="PingPongClient.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PingPongClient type.
// </summary>
// --------------------------------------------------------------------------------

namespace TransportTestClient
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;

    using ClosetRpc.Net;

    internal class PingPongClient
    {
        #region Fields

        private Channel channel;

        private SocketClientTransport clientTransport;

        private bool exitRequested;

        #endregion

        #region Public Methods and Operators

        public void Run()
        {
            Console.CancelKeyPress += this.ConsoleOnCancelKeyPress;
            while (!this.exitRequested)
            {
                Console.WriteLine("Attempting to connect to the server...");
                this.clientTransport = new SocketClientTransport("localhost", 3100);
                try
                {
                    this.channel = this.clientTransport.Connect();
                    this.SendMessages();
                    this.channel.Close();
                }
                catch (SocketException)
                {
                }
            }
        }

        #endregion

        #region Methods

        private void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            consoleCancelEventArgs.Cancel = true;
            this.exitRequested = true;
        }

        private string ReadMessage()
        {
            var headerBuffer = new byte[4];
            var bytesRead = this.channel.Stream.Read(headerBuffer, 0, headerBuffer.Length);
            if (bytesRead < headerBuffer.Length)
            {
                throw new Exception("Invalid message length");
            }

            var len = BitConverter.ToInt32(headerBuffer, 0);
            var dataBuffer = new byte[len];
            bytesRead = this.channel.Stream.Read(dataBuffer, 0, dataBuffer.Length);
            if (bytesRead < dataBuffer.Length)
            {
                throw new Exception("Invalid message length");
            }

            var encoder = new UTF8Encoding(false);
            return encoder.GetString(dataBuffer, 0, dataBuffer.Length);
        }

        private string SendMessage(string text)
        {
            var encoder = new UTF8Encoding(false);
            var messageBody = encoder.GetBytes(text);

            var messageHeader = BitConverter.GetBytes(messageBody.Length);

            this.channel.Stream.Write(messageHeader, 0, messageHeader.Length);
            this.channel.Stream.Write(messageBody, 0, messageBody.Length);

            return this.ReadMessage();
        }

        private void SendMessages()
        {
            Console.WriteLine("Connected. Press Ctrl+C to exit.");
            while (!this.exitRequested)
            {
                Console.Write(">");
                var text = Console.ReadLine();
                if (text == null)
                {
                    continue;
                }

                try

                {
                    var result = this.SendMessage(text);
                    Console.WriteLine(result);
                }
                catch (IOException)
                {
                    Console.WriteLine("error: Read error.");
                    break;
                }
                catch (SocketException)
                {
                    Console.WriteLine("error: Connection lost.");
                    break;
                }
            }
        }

        #endregion
    }
}
