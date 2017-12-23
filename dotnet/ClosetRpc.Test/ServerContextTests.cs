// --------------------------------------------------------------------------------
// <copyright file="ServerContextTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ServerContextTests type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using System.Threading;

    using ClosetRpc;

    using NUnit.Framework;

    [TestFixture]
    public class ServerContextTests
    {
        [Test]
        public void ServerContextTest()
        {
            var channel = new Channel(null);
            var thread = new Thread(() => { });
            var server = new RpcServer(null);

            var sc = new ServerContext(server, channel, thread);
            Assert.That(sc.Channel, Is.SameAs(channel));
            Assert.That(sc.Thread, Is.SameAs(thread));
            Assert.That(sc.ObjectManager, Is.Not.Null);
            Assert.That(sc.GlobalEventSource, Is.Not.Null);
            Assert.That(sc.LocalEventSource, Is.Not.Null);
        }
    }
}
