// --------------------------------------------------------------------------------
// <copyright file="ContactListService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the ContactListService type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    using System;
    using System.Linq;

    using ClosetRpc;

    using Google.Protobuf.WellKnownTypes;

    using NanoMessanger;

    public class ContactListService : ContactListService_ServiceBase
    {
        #region Fields

        private readonly BusinessLogic bl;

        #endregion

        #region Constructors and Destructors

        public ContactListService(BusinessLogic bl)
        {
            this.bl = bl;
        }

        #endregion

        #region Public Methods and Operators

        public override void AddContact(IServerContext context, ContactInfo value)
        {
            var user = this.GetSessionUser(context);
            var contact = this.bl.GetUser(value.Username);
            if (contact == null)
            {
                throw new Exception("Unknown user");
            }

            this.bl.AddContact(user, contact);
        }

        public override ContactList FindUser(IServerContext context, StringValue value)
        {
            var users = this.bl.SearchUsers(value.Value);
            var contactList = new ContactList();
            contactList.Contacts.AddRange(
                users.Select(
                    o => new ContactInfo { Username = o.UserName, FirstName = o.FirstName, LastName = o.LastName }));
            return contactList;
        }

        public override ContactList GetContacts(IServerContext context)
        {
            var user = this.GetSessionUser(context);

            var contactList = new ContactList();
            foreach (var contact in this.bl.GetContacts(user.UserName))
            {
                contactList.Contacts.Add(
                    new ContactInfo
                        {
                            Username = contact.UserName,
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                        });
            }

            return contactList;
        }

        public override void RemoveContact(IServerContext context, StringValue value)
        {
            var user = this.GetSessionUser(context);
            user.Contacts.Remove(value.Value);
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
