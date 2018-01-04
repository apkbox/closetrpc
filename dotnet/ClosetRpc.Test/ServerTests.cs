// --------------------------------------------------------------------------------
// <copyright file="ServerTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ServerTests type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    public class FakeServerTransport : IServerTransport
    {
        #region Public Methods and Operators

        public void Cancel()
        {
            return;
        }

        public IChannel Listen()
        {
            return new FakeServerChannel();
        }

        #endregion

        public class FakeServerChannel : IChannel
        {
            #region Fields

            private readonly MemoryStream stream = new MemoryStream();

            #endregion

            #region Public Properties

            public Stream Stream
            {
                get
                {
                    return this.stream;
                }
            }

            #endregion

            #region Public Methods and Operators

            public void Close()
            {
                this.stream.Close();
            }

            #endregion
        }
    }

    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void Test1()
        {
            var server = new RpcServer(new FakeServerTransport());
            var task = new Task(() => server.Run());
            task.Start();
            Thread.Sleep(1000);
            server.Shutdown(false);
        }
    }
}
