// --------------------------------------------------------------------------------
// <copyright file="SettingsEvents_Proxy.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SettingsEvents_Proxy type.
// </summary>
// --------------------------------------------------------------------------------

namespace ProtobufTestApp
{
    using ClosetRpc;

    using Google.Protobuf;

    using ProtobufTestApp.Services;

    public class SettingsEvents_Proxy
    {
        #region Static Fields

        private static readonly string ServiceName = "services.SettingsEvents";

        #endregion

        #region Constructors and Destructors

        public SettingsEvents_Proxy()
        {
        }

        public SettingsEvents_Proxy(Server server)
        {
            this.Server = server;
        }

        #endregion

        #region Public Properties

        public Server Server { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Changed(IEventSource source, SettingList value)
        {
            var call = this.Server.CreateCallBuilder();
            call.ServiceName = SettingsEvents_Proxy.ServiceName;
            call.MethodName = "Changed";
            call.CallData = value.ToByteArray();
            source.SendEvent(call);
        }

        #endregion
    }
}
