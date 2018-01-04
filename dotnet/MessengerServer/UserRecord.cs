// --------------------------------------------------------------------------------
// <copyright file="UserRecord.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the UserRecord type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    public class UserRecord
    {
        #region Public Properties

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        #endregion
    }
}
