using System.Configuration;
using System.Net.Configuration;

namespace App.Core.Configuration
{
    public class ConfigurationProvider: IConfigurationProvider
    {
        private string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);             
        }

        private SmtpSection GetSection(string sectionName)
        {
            SmtpSection cfg = (SmtpSection)ConfigurationManager.GetSection(sectionName);

            return cfg;
        }

        private string GetConnectionString(string name)
        {
            var setting = ConfigurationManager.ConnectionStrings[name];
            return (setting == null) ? string.Empty : setting.ConnectionString;
        }
        
        public string DirectoryProductImage => GetAppSetting("DirectoryProductImage").Trim();

        public string DirectoryProductThumbnail => GetAppSetting("DirectoryProductThumbnail").Trim();

        public string DirectoryGalleryImage => GetAppSetting("DirectoryGalleryImage").Trim();

        public string DirectoryGalleryThumbnail => GetAppSetting("DirectoryGalleryThumbnail").Trim();

        public string DefaultGalleryImage => GetAppSetting("DefaultGalleryImage").Trim();

        public string DefaultGalleryThumbnail => GetAppSetting("DefaultGalleryThumbnail").Trim();

        public int ThumbnailWidth => 420;

        public int ThumbnailPhotoWidth => 600;

        public string DirectoryLogo => GetAppSetting("DirectoryLogo").Trim();

        public string DirectoryProfileImage => GetAppSetting("DirectoryProfileImage").Trim();

        public string DirectoryProfileThumbnail => GetAppSetting("DirectoryProfileThumbnail").Trim();

        public string DefaultProfileImage => GetAppSetting("DefaultProfileImage").Trim();

        public string DefaultProfileThumbnail => GetAppSetting("DefaultProfileThumbnail").Trim();

        public int ProfileImageWidth => 360;
        public int ProfileThumbnailWidth => 150;

        public SmtpSection SmtpConfiguration => (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

    }
}