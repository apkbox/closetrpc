// --------------------------------------------------------------------------------
// <copyright file="PendingCallTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the InternalObjectsTests type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using ClosetRpc;

    using NUnit.Framework;

    [TestFixture]
    public class PendingCallTests
    {
        [Test]
        public void Properties()
        {
            var resultData = new RpcResultStub();

            var p = new PendingCall();
            Assert.That(p.Status, Is.EqualTo(PendingCallStatus.AwaitingResult));
            Assert.That(p.Result, Is.Null);

            p.Status = PendingCallStatus.Received;
            p.Result = resultData;

            Assert.That(p.Status, Is.EqualTo(PendingCallStatus.Received));
            Assert.That(p.Result, Is.SameAs(resultData));
        }

        private class RpcResultStub : IRpcResult
        {
            #region Public Properties

            public byte[] ResultData { get; set; }

            public RpcStatus Status { get; set; }

            #endregion
        }
    }
}
