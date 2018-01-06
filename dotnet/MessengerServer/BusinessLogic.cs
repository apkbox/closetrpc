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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class BusinessLogic
    {
        #region Fields

        private readonly Dictionary<string, User> sessions = new Dictionary<string, User>();

        private readonly Dictionary<string, User> users = new Dictionary<string, User>();

        #endregion

        #region Public Methods and Operators

        public void AddContact(User user, User contact)
        {
            user.Contacts[contact.UserName] = contact;
        }

        public void AddUser(User user)
        {
            if (this.users.ContainsKey(user.UserName))
            {
                throw new Exception("user already exists.");
            }

            this.users.Add(user.UserName, user);
        }

        public User GetUser(string userName)
        {
            return this.users.TryGetValue(userName, out var user) ? user : null;
        }

        public User GetUserBySessionKey(string sessionKey)
        {
            return this.sessions.TryGetValue(sessionKey, out var user) ? user : null;
        }

        public string Login(string userName, string password)
        {
            if (!this.users.TryGetValue(userName, out var user))
            {
                return null;
            }

            if (user.Password != password)
            {
                return null;
            }

            var sessionId = Guid.NewGuid().ToString();
            this.sessions[sessionId] = user;
            return sessionId;
        }

        public void Logout(string sessionId)
        {
            this.sessions.Remove(sessionId);
        }

        public void RemoveUser(string userName)
        {
            this.users.Remove(userName);
        }

        public List<User> SearchUsers(string search)
        {
            var selected = new HashSet<User>();

            foreach (var user in this.users.Values)
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

        #endregion

        public IList<User> GetContacts(string userName)
        {
            var user = this.users[userName];
            return user.Contacts.Values.ToList();
        }

        public void SendMessage(string fromUserName, string toUserName, string text)
        {
            var toUser = this.users[toUserName];
            toUser.AddMessage(fromUserName, text);
        }
    }
}
