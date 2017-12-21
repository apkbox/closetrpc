// --------------------------------------------------------------------------------
// <copyright file="ProtocolObjectsTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ProtocolObjects type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using System.IO;

    using ClosetRpc;
    using ClosetRpc.Protocol;

    using NUnit.Framework;

    [TestFixture]
    public class ProtocolObjectsTests
    {
        private readonly uint requestId = 5;

        private readonly string serviceName = "Service";

        private readonly string methodName = "Method";

        private readonly bool isAsync = true;

        private readonly ulong objectId = 42ul;

        private readonly byte[] callData = new byte[] { 1, 3, 5, 7 };

        [Test]
        public void RpcCallDefaults()
        {
            var p = new ProtocolObjectFactory();
            var c = p.CreateRpcCall();

            Assert.That(c, Is.Not.Null);
            Assert.That(c.ServiceName, Is.Null);
            Assert.That(c.MethodName, Is.Null);
            Assert.That(c.IsAsync, Is.False);
            Assert.That(c.ObjectId, Is.EqualTo(0));
            Assert.That(c.CallData, Is.Null);
        }

        [Test]
        public void CallBuilderDefaults()
        {
            var p = new ProtocolObjectFactory();
            var c = new RpcCallParameters();

            Assert.That(c, Is.Not.Null);
            Assert.That(c.ServiceName, Is.Null);
            Assert.That(c.MethodName, Is.Null);
            Assert.That(c.IsAsync, Is.False);
            Assert.That(c.ObjectId, Is.EqualTo(0));
            Assert.That(c.CallData, Is.Null);
        }

        [Test]
        public void CallBuilderProperties()
        {
            var p = new ProtocolObjectFactory();
            var c = new RpcCallParameters();

            Assume.That(c, Is.Not.Null);

            c.ServiceName = this.serviceName;
            c.MethodName = this.methodName;
            c.IsAsync = this.isAsync;
            c.ObjectId = this.objectId;
            c.CallData = this.callData;

            Assert.That(c.ServiceName, Is.EqualTo(this.serviceName));
            Assert.That(c.MethodName, Is.EqualTo(this.methodName));
            Assert.That(c.IsAsync, Is.EqualTo(this.isAsync));
            Assert.That(c.ObjectId, Is.EqualTo(this.objectId));
            Assert.That(c.CallData, Is.EquivalentTo(this.callData));
        }

        [Test]
        public void EmptyMessage()
        {
            var p = new ProtocolObjectFactory();

            byte[] buffer;
            using (var s = new MemoryStream())
            {
                p.WriteMessage(s, this.requestId, null, null);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var message = p.RpcMessageFromStream(s);
                Assert.That(message.RequestId, Is.EqualTo(this.requestId));
                Assert.That(message.Call, Is.Null);
                Assert.That(message.Result, Is.Null);
                Assert.That(s.Position, Is.EqualTo(s.Length));
            }
        }

        [Test]
        public void MessageWithUninitializedCall()
        {
            var p = new ProtocolObjectFactory();

            byte[] buffer;
            using (var s = new MemoryStream())
            {
                var call = p.BuildCall(new RpcCallParameters());
                p.WriteMessage(s, this.requestId, call, null);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var message = p.RpcMessageFromStream(s);
                Assert.That(message.RequestId, Is.EqualTo(this.requestId));
                Assert.That(message.Call, Is.Not.Null);
                Assert.That(message.Result, Is.Null);
                Assert.That(s.Position, Is.EqualTo(s.Length));

                Assert.That(0, Is.EqualTo(message.Call.ObjectId));
                Assert.That(message.Call.ServiceName, Is.Empty);
                Assert.That(message.Call.MethodName, Is.Empty);
                Assert.That(message.Call.IsAsync, Is.False);
                Assert.That(message.Call.ObjectId, Is.EqualTo(0));
                Assert.That(message.Call.CallData, Is.Empty);
            }
        }

        [Test]
        public void MessageWithResult()
        {
            var p = new ProtocolObjectFactory();

            byte[] buffer;
            using (var s = new MemoryStream())
            {
                var result = p.CreateRpcResult();
                p.WriteMessage(s, this.requestId, null, result);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var message = p.RpcMessageFromStream(s);
                Assert.That(this.requestId, Is.EqualTo(message.RequestId));
                Assert.That(message.Call, Is.Null);
                Assert.That(message.Result, Is.Not.Null);
                Assert.That(s.Position, Is.EqualTo(s.Length));

                Assert.That(message.Result.Status, Is.EqualTo(RpcStatus.Succeeded));
                Assert.That(message.Result.ResultData, Is.Empty);
            }
        }

        [Test]
        public void CallMessageFromCallBuilder()
        {
            var p = new ProtocolObjectFactory();

            byte[] buffer;
            using (var s = new MemoryStream())
            {
                var call = new RpcCallParameters();
                call.ServiceName = this.serviceName;
                call.MethodName = this.methodName;
                call.IsAsync = this.isAsync;
                call.ObjectId = this.objectId;
                call.CallData = this.callData;
                var c = p.BuildCall(call);
                p.WriteMessage(s, this.requestId, c, null);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var message = p.RpcMessageFromStream(s);
                Assert.That(s.Position, Is.EqualTo(s.Length));

                Assert.That(message.RequestId, Is.EqualTo(this.requestId));
                Assert.That(message.Call, Is.Not.Null);
                Assert.That(message.Result, Is.Null);

                Assert.That(message.Call.ObjectId, Is.EqualTo(this.objectId));
                Assert.That(message.Call.ServiceName, Is.EqualTo(this.serviceName));
                Assert.That(message.Call.MethodName, Is.EqualTo(this.methodName));
                Assert.That(message.Call.IsAsync, Is.EqualTo(this.isAsync));
                Assert.That(message.Call.CallData, Is.EquivalentTo(this.callData));
            }
        }
    }
}
