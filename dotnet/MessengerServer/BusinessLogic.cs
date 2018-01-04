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

        public readonly Dictionary<string, UserRecord> Users = new Dictionary<string, UserRecord>();

        #endregion

        #region Public Methods and Operators

        public void AddUser(UserRecord userRecord)
        {
            if (this.Users.ContainsKey(userRecord.UserName))
            {
                throw new Exception("User already exists.");
            }

            this.Users.Add(userRecord.UserName, userRecord);
        }

        public void RemoveUser(string userName)
        {
            this.Users.Remove(userName);
        }

        #endregion
    }
}
