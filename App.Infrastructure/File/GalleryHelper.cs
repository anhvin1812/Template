using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using App.Core.Configuration;


namespace App.Infrastructure.File
{
    using File = System.IO.File;

    public static class GalleryHelper
    {
        #region Gallery
        public static string GetImagePath(string fileName)
        {
            var imgPath = string.IsNullOrWhiteSpace(fileName)
                ? AppSettings.ConfigurationProvider.DefaultGalleryImage
                : $"{AppSettings.ConfigurationProvider.DirectoryGalleryImage}/{fileName}";

            return imgPath;
        }

        public static string GetThumbnailPath(string fileName)
        {
            var imgPath = string.IsNullOrWhiteSpace(fileName)
                ? AppSettings.ConfigurationProvider.DefaultGalleryThumbnail
                : $"{AppSettings.ConfigurationProvider.DirectoryGalleryThumbnail}/{fileName}";

            return imgPath;
        }


        public static string UploadGallery(HttpPostedFileBase image, int thumbWidth)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "File can not be null.");

            var imageName = $"Gallery_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{ Path.GetExtension(image.FileName)}";
            Image img = Image.FromStream(image.InputStream);
            Image thumb = img;

            if (img.Width > thumbWidth)
            {
                var thumbHeight = thumbWidth * img.Height / img.Width;
                thumb = ResizeImage(img, thumbWidth, thumbHeight);
            }

            var fullPath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}");

            var index = 1;
            while (File.Exists(fullPath))
            {
                imageName = $"Gallery_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}_{index}{ Path.GetExtension(image.FileName)}";
                fullPath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}");
                index++;
            }

            img.Save(HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}"));
            thumb.Save(HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryGalleryThumbnail}/{imageName}"));

            return imageName;
        }

        public static void DeleteGallery(string imageFileName, string thumbnailFileName)
        {
            // Delete image
            var fullImagePath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryGalleryImage}/{imageFileName}");

            if (File.Exists(fullImagePath))
            {
                File.Delete(fullImagePath);
            }

            // Delete thumbnail
            var fullThumbnailPath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryGalleryThumbnail}/{thumbnailFileName}");
            if (File.Exists(fullThumbnailPath))
            {
                File.Delete(fullThumbnailPath);
            }
        }

        #endregion

        #region Logo
        public static string GetLogoPath(string fileName)
        {
            var imgPath = $"{AppSettings.ConfigurationProvider.DirectoryLogo}/{fileName}";
            return imgPath;
        }

        public static string UploadLogo(HttpPostedFileBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "File can not be null.");

            var imageName = $"Logo{ Path.GetExtension(image.FileName)}";
            Image img = Image.FromStream(image.InputStream);
            
            var fullPath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryLogo}/{imageName}");

            // Delete image
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            img.Save(fullPath);

            return imageName;
        }
        #endregion

        #region Utilities
        public static string ToBase64String(this HttpPostedFileBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "File can not be null.");

            using (var theReader = new BinaryReader(image.InputStream))
            {
                var bytes = theReader.ReadBytes(image.ContentLength);
                var imageBase64 = Convert.ToBase64String(bytes);

                return imageBase64;
            }
        }   
        #endregion

        #region Private Methods
        private static Bitmap ResizeImage(Image originalImage, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight));
            }

            return newImage;
        }
        #endregion
    }
}
