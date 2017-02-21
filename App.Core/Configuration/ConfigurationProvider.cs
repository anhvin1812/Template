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
        
        public string DirectoryProductImage
        {
            get
            {
                return GetAppSetting("DirectoryProductImage").Trim();
            }
        }

        public string DirectoryProductThumbnail
        {
            get
            {
                return GetAppSetting("DirectoryProductThumbnail").Trim();
            }
        }

        public string DirectoryGalleryImage
        {
            get
            {
                return GetAppSetting("DirectoryGalleryImage").Trim();
            }
        }

        public string DirectoryGalleryThumbnail
        {
            get
            {
                return GetAppSetting("DirectoryGalleryThumbnail").Trim();
            }
        }

        public string DefaultGalleryImage
        {
            get
            {
                return GetAppSetting("DefaultGalleryImage").Trim();
            }
        }

        public string DefaultGalleryThumbnail
        {
            get
            {
                return GetAppSetting("DefaultGalleryThumbnail").Trim();
            }
        }
    }
}