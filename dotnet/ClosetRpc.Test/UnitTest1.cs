namespace ClosetRpc.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClosetRpc.Net.Protocol;
    using System.IO;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EmptyMessage()
        {
            var p = new ProtocolObjectFactory();

            byte[] buffer;
            using (var s = new MemoryStream())
            {
                p.WriteMessage(s, 5, null, null);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var message = p.RpcMessageFromStream(s);
                Assert.AreEqual(5u, message.RequestId);
                Assert.IsNull(message.Call);
                Assert.IsNull(message.Result);
                Assert.AreEqual(s.Length, s.Position);
            }
        }

        [TestMethod]
        public void MessageWithCall()
        {
            var p = new ProtocolObjectFactory();

            byte[] buffer;
            using (var s = new MemoryStream())
            {
                var call = p.CreateCallBuilder();
                p.WriteMessage(s, 5, call, null);
                buffer = s.ToArray();
            }

            using (var s = new MemoryStream(buffer))
            {
                var message = p.RpcMessageFromStream(s);
                Assert.AreEqual(5u, message.RequestId);
                Assert.IsNotNull(message.Call);
                Assert.IsNull(message.Result);
                Assert.AreEqual(s.Length, s.Position);

                Assert.AreEqual(0u, message.Call.ObjectId);
                Assert.IsNull(message.Call.ServiceName);
                Assert.IsNull(message.Call.MethodName);
                Assert.IsFalse(message.Call.IsAsync);
                Assert.IsNull(message.Call.CallData);
            }
        }
    }
}
