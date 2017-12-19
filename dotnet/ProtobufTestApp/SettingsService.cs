namespace ProtobufTestApp
{
    using System.Collections.Generic;

    using ClosetRpc.Net;

    using ProtobufTestApp.Services;

    public class SettingsService : SettingsService_ServiceBase
    {
        #region Fields

        private readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        #endregion

        #region Public Methods and Operators

        public override SettingList Get(ServerContext context, SettingKeyList value)
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

        public override void Set(ServerContext context, SettingList value)
        {
            foreach (var item in value.Item)
            {
                this.settings[item.Key] = item.Value;
            }
        }

        #endregion
    }
}
