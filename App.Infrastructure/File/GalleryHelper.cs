using System;
using System.Drawing;
using System.IO;
using System.Web;
using App.Core.Configuration;


namespace App.Infrastructure.File
{
    using File = System.IO.File;

    public static class GalleryHelper
    {
        public static string GetImagePath(string fileName)
        {
            var imgPath = string.IsNullOrWhiteSpace(fileName) 
                ? Settings.ConfigurationProvider.DefaultGalleryImage 
                : $"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{fileName}";

            return imgPath;
        }

        public static string GetThumbnailPath(string fileName)
        {
            var imgPath = string.IsNullOrWhiteSpace(fileName)
                ? Settings.ConfigurationProvider.DefaultGalleryThumbnail
                : $"{Settings.ConfigurationProvider.DirectoryGalleryThumbnail}/{fileName}";

            return imgPath;
        }


        public static string UploadGallery(HttpPostedFileBase image)
        {
            if(image ==null)
                throw new ArgumentNullException(nameof(image), "File can not be null.");

            var imageName = $"Gallery_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{ Path.GetExtension(image.FileName)}";
            Image img = Image.FromStream(image.InputStream);
            Image thumb = img.GetThumbnailImage(270, 270, () => false, IntPtr.Zero);

            var fullPath = HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}");

            var index = 1;
            while (File.Exists(fullPath))
            {
                imageName = $"Gallery_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}_{index}{ Path.GetExtension(image.FileName)}";
                fullPath = HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}");
                index++;
            }

            img.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}"));
            thumb.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryThumbnail}/{imageName}"));
            
            return imageName;
        }

        public static void DeleteGallery(string imageFileName, string thumbnailFileName)
        {
            // Delete image
            var fullImagePath = HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{imageFileName}");

            if (File.Exists(fullImagePath))
            {
                File.Delete(fullImagePath);
            }

            // Delete thumbnail
            var fullThumbnailPath = HttpContext.Current.Server.MapPath( $"{Settings.ConfigurationProvider.DirectoryGalleryThumbnail}/{thumbnailFileName}");
            if (File.Exists(fullThumbnailPath))
            {
                File.Delete(fullThumbnailPath);
            }
        }
    }
}
