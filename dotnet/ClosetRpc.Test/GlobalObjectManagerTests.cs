// --------------------------------------------------------------------------------
// <copyright file="GlobalObjectManagerTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the GlobalObjectManagerTests type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using System;

    using ClosetRpc;

    using NUnit.Framework;

    [TestFixture]
    public class GlobalObjectManagerTests
    {
        [Test]
        public void RefuseToRegisterNullService()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(null), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterServiceWithNullName()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(new RpcService(null)), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterServiceWithDuplicateName()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(new RpcService("Service1")), Throws.Nothing);
            Assert.That(() => mgr.RegisterService(new RpcService("Service1")), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void RefuseToRegisterNullServiceStub()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(null, "Service1"), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterServiceStubWithNullName()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(new RpcServiceStub(), null), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterServiceStubWithAllNullParameters()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterServiceStubWithDuplicateName()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(new RpcServiceStub(), "Service1"), Throws.Nothing);
            Assert.That(() => mgr.RegisterService(new RpcServiceStub(), "Service1"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ServiceAndServiceStubsBelongToTheSameNamespace()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(() => mgr.RegisterService(new RpcServiceStub(), "Service1"), Throws.Nothing);
            Assert.That(() => mgr.RegisterService(new RpcService("Service1")), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void RegisterDistinctServices()
        {
            var service1 = new RpcService("Service1");
            var service2 = new RpcService("Service2");
            var service3 = new RpcServiceStub();

            var mgr = new GlobalObjectManager();
            mgr.RegisterService(service1);
            mgr.RegisterService(service2);
            mgr.RegisterService(service3, "Service3");

            Assert.That(mgr.GetService("Service1"), Is.SameAs(service1));
            Assert.That(mgr.GetService("Service2"), Is.SameAs(service2));
            Assert.That(mgr.GetService("Service3"), Is.SameAs(service3));
        }

        [Test]
        public void UnknownServiceRequestShouldReturnNull()
        {
            var mgr = new GlobalObjectManager();
            Assert.That(mgr.GetService("Service1"), Is.Null);
        }

        private class RpcServiceStub : IRpcServiceStub
        {
            #region Public Methods and Operators

            public void CallMethod(IServerContext context, IRpcCall rpcCall, IRpcResult rpcResult)
            {
            }

            #endregion
        }

        private class RpcService : IRpcService
        {
            #region Constructors and Destructors

            public RpcService(string name)
            {
                this.Name = name;
            }

            #endregion

            #region Public Properties

            public string Name { get; }

            #endregion

            #region Public Methods and Operators

            public void CallMethod(IServerContext context, IRpcCall rpcCall, IRpcResult rpcResult)
            {
            }

            #endregion
        }
    }
}
