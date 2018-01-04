// --------------------------------------------------------------------------------
// <copyright file="BusinessLogic.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the BusinessLogic type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    using System;
    using System.Collections.Generic;

    public class BusinessLogic
    {
        #region Fields

        public readonly Dictionary<string, User> Users = new Dictionary<string, User>();

        public readonly Dictionary<string, User> Sessions = new Dictionary<string, User>();

        #endregion

        #region Public Methods and Operators

        public void AddUser(User User)
        {
            if (this.Users.ContainsKey(User.UserName))
            {
                throw new Exception("User already exists.");
            }

            this.Users.Add(User.UserName, User);
        }

        public void RemoveUser(string userName)
        {
            this.Users.Remove(userName);
        }

        public string Login(string userName, string password)
        {
            if (!this.Users.TryGetValue(userName, out var user))
                return null;

            if (user.Password != password)
                return null;

            var sessionId = Guid.NewGuid().ToString();
            this.Sessions[sessionId] = user;
            return sessionId;
        }

        public void Logout(string sessionId)
        {
            this.Sessions.Remove(sessionId);
        }

        public List<User> SearchContact(string search)
        {
            HashSet<User> selected = new HashSet<User>();

            foreach(var user in Users.Values)
            {
                if (user.UserName.Contains(search))
                {
                    selected.Add(user);
                }
                else if (user.LastName.Contains(search))
                {
                    selected.Add(user);
                }
                if (user.FirstName.Contains(search))
                {
                    selected.Add(user);
                }
            }

            return new List<User>(selected);
        }

        public void AddContact(User user, User contact)
        {
            user.Contacts.[contact.UserName] = contact;
        }

        #endregion
    }
}
