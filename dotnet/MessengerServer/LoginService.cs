// --------------------------------------------------------------------------------
// <copyright file="LoginService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the LoginService type.
// </summary>
// --------------------------------------------------------------------------------

namespace MessengerServer
{
    using System;

    using ClosetRpc;

    using Google.Protobuf.WellKnownTypes;

    using NanoMessanger;

    public class LoginService : LoginService_ServiceBase
    {
        #region Fields

        private readonly BusinessLogic bl;

        #endregion

        #region Constructors and Destructors

        public LoginService(BusinessLogic bl)
        {
            this.bl = bl;
        }

        #endregion

        #region Public Methods and Operators

        public override BoolValue ChangeAuthenticationInfo(IServerContext context, RegistrationData value)
        {
            throw new NotImplementedException();
        }

        public override SessionInfo Login(IServerContext context, AuthenticationData value)
        {
            var sessionKey = this.bl.Login(value.Username, value.Password);
            return new SessionInfo { SessionKey = sessionKey };
        }

        public override BoolValue Logout(IServerContext context)
        {
            throw new NotImplementedException();
        }

        public override BoolValue Reconnect(IServerContext context, StringValue value)
        {
            throw new NotImplementedException();
        }

        public override BoolValue Register(IServerContext context, RegistrationData value)
        {
            var user = new User
                           {
                               UserName = value.Username,
                               Password = value.Password,
                               Email = value.Email,
                               FirstName = value.FirstName,
                               LastName = value.LastName,
                           };
            try
            {
                this.bl.AddUser(user);
            }
            catch (Exception)
            {
                return new BoolValue { Value = false };
            }

            return new BoolValue { Value = true };
        }

        #endregion
    }
}
