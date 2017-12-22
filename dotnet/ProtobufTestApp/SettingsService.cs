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
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using ClosetRpc;

    using ProtobufTestApp.Services;

    public class SettingsService : SettingsService_ServiceBase, IDisposable
    {
        #region Fields

        private readonly IEventSource globalSource;

        private readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        private readonly SettingsEvents_EventProxy settingsEvents = new SettingsEvents_EventProxy();

        private readonly object settingsLock = new object();

        private readonly Timer timer;

        #endregion

        #region Constructors and Destructors

        public SettingsService(IEventSource globalSource)
        {
            this.timer = new Timer(this.OnTimer, null, 0, 1000);
            this.globalSource = globalSource;
        }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.timer?.Dispose();
        }

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

        #region Methods

        private void OnTimer(object state)
        {
            var time = DateTime.Now.ToLongTimeString();
            lock (this.settingsLock)
            {
                this.settings["currentTime"] = time;
            }

            var listOfChanges = new SettingList { Item = { new Setting { Key = "currentTime", Value = time } } };

            this.settingsEvents.Changed(this.globalSource, listOfChanges);
        }

        #endregion
    }
}
