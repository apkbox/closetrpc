// --------------------------------------------------------------------------------
// <copyright file="EventServiceManagerTests.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the EventServiceManagerTests type.
// </summary>
// --------------------------------------------------------------------------------

namespace ClosetRpc.Test
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class EventServiceManagerTests
    {
        public class EventHandler : IEventHandler
        {
            #region Constructors and Destructors

            public EventHandler(string name)
            {
                this.Name = name;
            }

            #endregion

            #region Public Properties

            public string Name { get; }

            #endregion

            #region Public Methods and Operators

            public void CallMethod(IRpcCall rpcCall)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public class EventHandlerStub : IEventHandlerStub
        {
            #region Public Methods and Operators

            public void CallMethod(IRpcCall rpcCall)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        [Test]
        public void RefuseToRegisterNullHandler()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(null), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterHandlerWithNullName()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(new EventHandler(null)), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterEventsWithDuplicateName()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(new EventHandler("Handler1")), Throws.Nothing);
            Assert.That(() => esm.RegisterHandler(new EventHandler("Handler1")), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void RefuseToRegisterNullHandlerStub()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(null, "Handler1"), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterHandlerStubWithNullName()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(new EventHandlerStub(), null), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterHandlerStubWithAllNullParameters()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(null, null), Throws.ArgumentNullException);
        }

        [Test]
        public void RefuseToRegisterHandlerStubWithDuplicateName()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(new EventHandlerStub(), "Handler"), Throws.Nothing);
            Assert.That(
                () => esm.RegisterHandler(new EventHandlerStub(), "Handler"),
                Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void HandlerAndHandlerStubsBelongToTheSameNamespace()
        {
            var esm = new EventServiceManager();
            Assert.That(() => esm.RegisterHandler(new EventHandlerStub(), "Handler1"), Throws.Nothing);
            Assert.That(() => esm.RegisterHandler(new EventHandler("Handler1")), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void RegisterDistinctHandlers()
        {
            var handler1 = new EventHandler("Handler1");
            var handler2 = new EventHandler("Handler2");
            var handler3 = new EventHandlerStub();

            var esm = new EventServiceManager();
            esm.RegisterHandler(handler1);
            esm.RegisterHandler(handler2);
            esm.RegisterHandler(handler3, "Handler3");

            Assert.That(esm.GetHandler("Handler1"), Is.SameAs(handler1));
            Assert.That(esm.GetHandler("Handler2"), Is.SameAs(handler2));
            Assert.That(esm.GetHandler("Handler3"), Is.SameAs(handler3));
        }

        [Test]
        public void UnknownHandlerRequestShouldReturnNull()
        {
            var esm = new EventServiceManager();
            Assert.That(esm.GetHandler("Handler1"), Is.Null);
        }
    }
}
