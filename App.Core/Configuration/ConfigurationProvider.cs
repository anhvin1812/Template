using System.Configuration;

namespace App.Core.Configuration
{
    public class ConfigurationProvider: IConfigurationProvider
    {
        private string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);             
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
    }
}