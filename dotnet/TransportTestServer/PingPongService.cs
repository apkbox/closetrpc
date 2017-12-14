// --------------------------------------------------------------------------------
// <copyright file="PingPongService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the PingPongService type.
// </summary>
// --------------------------------------------------------------------------------

namespace TransportTestServer
{
    using System;
    using System.IO;

    using ClosetRpc.Net;

    public class PingPongService
    {
        #region Static Fields

        private static int ClientId = 0;

        #endregion

        #region Fields

        private readonly Channel channel;

        private int clientId;

        #endregion

        #region Constructors and Destructors

        public PingPongService(Channel channel)
        {
            this.channel = channel;
        }

        #endregion

        #region Public Properties

        public bool IsRunning { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Run()
        {
            this.IsRunning = true;
            this.clientId = ++PingPongService.ClientId;
            Console.WriteLine("Client {0} connected.", this.clientId);
            try
            {
                while (this.ReadMessage())
                {
                }
            }
            catch (Exception)
            {
                Console.WriteLine("error: Client {0} disconnected", this.clientId);
            }

            this.channel.Close();
            this.IsRunning = false;
            Console.WriteLine("Client {0} handler exiting", this.clientId);
        }

        public void Stop()
        {
            this.channel.Close();
        }

        #endregion

        #region Methods

        private bool ReadMessage()
        {
            // TODO: Handle partial reads.
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
                throw new IOException("Invalid message length");
            }

            Console.WriteLine("{0}: received message", this.clientId);

            headerBuffer = BitConverter.GetBytes(len);
            this.channel.Stream.Write(headerBuffer, 0, headerBuffer.Length);
            this.channel.Stream.Write(dataBuffer, 0, len);

            Console.WriteLine("{0}: sent reply", this.clientId);
            return true;
        }

        #endregion
    }
} // namespace TransportTestServer
