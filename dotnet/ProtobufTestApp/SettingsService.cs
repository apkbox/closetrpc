// --------------------------------------------------------------------------------
// <copyright file="SettingsService.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   Defines the SettingsService type.
// </summary>
// --------------------------------------------------------------------------------

namespace ProtobufTestApp
{
    using System.Collections.Generic;

    using ClosetRpc;

    using ProtobufTestApp.Services;

    #region Event classes

    //public class SettingsEvents_Proxy
    //{
    //    private static readonly string ServiceName = "services.SettingsEvents";

    //    public void Changed(IEventSource source, SettingList value)
    //    {
    //        var call = source.CreateCallBuilder();
    //        call.ServiceName = SettingsEvents_Proxy.ServiceName;
    //        call.MethodName = "Changed";
    //        call.CallData = value.ToByteArray();
    //        var result = source.SendEvent(call);
    //        if (result.Status != global::ClosetRpc.RpcStatus.Succeeded)
    //        {
    //            throw new Exception(); // TODO: Be more specific
    //        }

    //    }

    //}

    #endregion



    public class SettingsService : SettingsService_ServiceBase
    {
        #region Fields

        // private SettingsEvents_Proxy settingsEvents = new SettingsEvents_Proxy();

        private readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        #endregion

        #region Public Methods and Operators

        public override SettingList Get(IServerContext context, SettingKeyList value)
        {
            var result = new SettingList();
            foreach (var item in value.Item)
            {
                if (this.settings.TryGetValue(item, out var settingValue))
                {
                    result.Item.Add(new Setting() { Key = item, Value = settingValue });
                }
            }

            return result;
        }

        public override void Set(IServerContext context, SettingList value)
        {
            var settingsList = new SettingList();

            foreach (var item in value.Item)
            {
                this.settings[item.Key] = item.Value;
                settingsList.Item.Add(item);
            }

            // this.settingsEvents.Changed(context.GlobalEventSource, settingsList);
        }

        #endregion
    }
}
