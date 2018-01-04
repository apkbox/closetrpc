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
    using ClosetRpc;

    using Google.Protobuf.WellKnownTypes;

    using NanoMessanger;
    public class LoginService : LoginService_ServiceBase
    {
        public override BoolValue Register(IServerContext context, RegistrationData value)
        {
            throw new System.NotImplementedException();
        }

        public override SessionInfo Login(IServerContext context, AuthenticationData value)
        {
            throw new System.NotImplementedException();
        }

        public override BoolValue Logout(IServerContext context)
        {
            throw new System.NotImplementedException();
        }

        public override BoolValue ChangeAuthenticationInfo(IServerContext context, RegistrationData value)
        {
            throw new System.NotImplementedException();
        }

        public override BoolValue Reconnect(IServerContext context, StringValue value)
        {
            throw new System.NotImplementedException();
        }
    }
}
