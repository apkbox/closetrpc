// --------------------------------------------------------------------------------
// <copyright file="UserRecord.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the UserRecord type.
// </summary>
// --------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MessengerServer
{
    public class PendingMessage
    {
        public int Ordinal { get; set; }
        public string FromUser;
        public string Text;
    }

    public class User
    {
        public Dictionary<string, User> Contacts { get; } = new Dictionary<string, User>();

        public List<PendingMessage> PendingMessages { get; } = new List<PendingMessage>();

        #region Public Properties

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        #endregion
    }
}
