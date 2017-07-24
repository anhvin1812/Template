
namespace App.Core.Configuration
{
    /// <summary>
    /// Sherpa configuration settings
    /// </summary>
    public static class AppSettings
    {
        public static ConfigurationProvider ConfigurationProvider;

        static AppSettings()
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
