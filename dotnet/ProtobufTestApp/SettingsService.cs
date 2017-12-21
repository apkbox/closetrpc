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

    public class SettingsService : SettingsService_ServiceBase
    {
        #region Fields

        private readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        private SettingsEvents_Proxy settingsEvents = new SettingsEvents_Proxy();

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

            this.settingsEvents.Changed(context.GlobalEventSource, settingsList);
        }

        #endregion
    }
}
