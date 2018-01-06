// --------------------------------------------------------------------------------
// <copyright file="User.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the User type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    using System.Collections.Generic;

    public class User
    {
        #region Fields

        private int messageOrdinal = 0;

        private object messageOrdinalLock = new object();

        #endregion

        #region Public Properties

        public Dictionary<string, User> Contacts { get; } = new Dictionary<string, User>();

        public string Email { get; set; }

        public string FirstName { get; set; }

        public bool IsOnline { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public List<PendingMessage> PendingMessages { get; } = new List<PendingMessage>();

        public string UserName { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddMessage(string fromUser, string text)
        {
            var messageId = 0;

            lock (this.messageOrdinalLock)
            {
                messageId = ++this.messageOrdinal;
            }

            var message = new PendingMessage() { FromUser = fromUser, Ordinal = messageId, Text = text };
            this.PendingMessages.Add(message);
        }

        #endregion
    }
}
