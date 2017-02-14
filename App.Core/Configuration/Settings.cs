using Sherpa.Core;

namespace App.Core.Configuration
{
    /// <summary>
    /// Sherpa configuration settings
    /// </summary>
    public static class Settings
    {
        public static ConfigurationProvider ConfigurationProvider;

        static Settings()
        {
            ConfigurationProvider = new ConfigurationProvider();
        }

        /// <summary>
        /// Smtp host.
        /// </summary>
        //public static string SmtpServer
        //{
        //    get
        //    {
        //        return _configurationProvider.GetAppSetting("SmtpClient");
        //    }
        //}
    }
}
