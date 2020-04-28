using System.Configuration;

namespace V1ToADOMigration.Core.Helpers
{
    public static class ConfigHelper
    {
        public static string V1Url { get; set; }
        public static string V1Name { get; set; }
        public static string V1Version { get; set; }
        public static string V1Username { get; set; }
        public static string V1Password { get; set; }
        public static string ADOUrl { get; set; }
        public static string ADOToken { get; set; }

        static ConfigHelper()
        {
            V1Url = GetAppSetting("V1Url");
            V1Name = GetAppSetting("V1Name");
            V1Version = GetAppSetting("V1Version");
            V1Username = GetAppSetting("V1Username");
            V1Password = GetAppSetting("V1Password");
            ADOUrl = GetAppSetting("ADOUrl");
            ADOToken = GetAppSetting("ADOToken");
        }
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
