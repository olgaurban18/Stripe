using System.Configuration;
using System.Linq;

namespace WcfService1
{
    public static class AppSettingsHelper
    {
        public static string GetSettingValue(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key) 
                ? ConfigurationManager.AppSettings[key] 
                : string.Empty;
        }
    }
}