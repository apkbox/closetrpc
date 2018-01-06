// --------------------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the MessageService type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    using System;
    using System.Linq;

    using ClosetRpc;

    using Google.Protobuf.WellKnownTypes;

    using NanoMessanger;

    internal class MessageService : MessageService_ServiceBase
    {
        #region Fields

        private readonly BusinessLogic bl;

        private readonly MessageEvents_EventProxy messageEvents = new MessageEvents_EventProxy();

        #endregion

        #region Constructors and Destructors

        public MessageService(BusinessLogic bl)
        {
            this.bl = bl;
        }

        #endregion

        #region Public Methods and Operators

        public override MessageList GetPendingMessages(IServerContext context, Int32Value value)
        {
            var user = this.GetSessionUser(context);
            var unsentMessages = user.PendingMessages.Where(o => o.Ordinal > value.Value)
                .Select(o => new Message { Text = o.Text, Username = o.FromUser, Ordinal = o.Ordinal });
            var messageList = new MessageList();
            messageList.Messages.AddRange(unsentMessages);
            return messageList;
        }

        public override void SendMessage(IServerContext context, Message value)
        {
            this.bl.SendMessage(context.UserData as string, value.Username, value.Text);
            var toUserConnection = context.ActiveConnections.FirstOrDefault(
                o =>
                    {
                        var sessionKey = o.UserData as string;
                        return sessionKey == value.Username;
                    });

            var eventSource = toUserConnection?.LocalEventSource;
            if (eventSource != null)
            {
                this.messageEvents.NewMessage(eventSource);
            }
        }

        #endregion

        #region Methods

        private User GetSessionUser(IServerContext context)
        {
            if (!(context.UserData is string sessionKey))
            {
                throw new Exception("No session.");
            }

            var user = this.bl.GetUserBySessionKey(sessionKey);
            if (user == null)
            {
                throw new Exception("No session.");
            }

            return user;
        }

        #endregion
    }
}
