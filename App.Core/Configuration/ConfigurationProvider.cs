using System.Configuration;
using App.Core.Configuration;

namespace Sherpa.Core
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
    }
}