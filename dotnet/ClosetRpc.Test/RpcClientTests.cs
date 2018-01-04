// --------------------------------------------------------------------------------
// <copyright file="RpcClientTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the RpcClientTests type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using System.IO;

    using NUnit.Framework;

    [TestFixture]
    [Ignore("Not yet implemented")]
    public class RpcClientTests
    {
        [Test]
        public void Test1()
        {
            var client = new RpcClient(new FakeClientTransport());
            client.Connect();
            var callMessage = new RpcCallParameters
                                  {
                                      ServiceName = "Service",
                                      MethodName = "Test",
                                      CallData = new byte[] { 1, 3, 5, 7 }
                                  };
            var result = client.CallService(callMessage);
            Assert.That(result.Status, Is.EqualTo(RpcStatus.Succeeded));
            Assert.That(result.ResultData, Is.EquivalentTo(new byte[] { 2, 4, 6, 8 }));
            client.Shutdown(false);
        }
    }

    public class FakeClientTransport : IClientTransport
    {
        #region Public Methods and Operators

        public IChannel Connect()
        {
            return new FakeClientChannel();
        }

        #endregion

        public class FakeClientChannel : IChannel
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
}
