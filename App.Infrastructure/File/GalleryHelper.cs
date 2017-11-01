using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Web;
using App.Core.Configuration;
using App.Core.Files;
using App.Core.News;


namespace App.Infrastructure.File
{
    using File = System.IO.File;

    public static class GalleryHelper
    {
        #region Gallery
        public static string GetFilePath(string fileName, FilePath filePath)
        {
            var fullPath = string.Empty;

            switch (filePath)
            {
                case FilePath.News:
                    fullPath = string.IsNullOrWhiteSpace(fileName)
                        ? AppSettings.ConfigurationProvider.DefaultGalleryImage
                        : $"{AppSettings.ConfigurationProvider.DirectoryNewsImage}/{fileName}";
                    break;
                case FilePath.NewsThumbnail:
                    fullPath = string.IsNullOrWhiteSpace(fileName) 
                        ? AppSettings.ConfigurationProvider.DefaultGalleryThumbnail 
                        : $"{AppSettings.ConfigurationProvider.DirectoryGalleryThumbnail}/{fileName}";
                    break;
                case FilePath.NewsImage:
                    fullPath = string.IsNullOrWhiteSpace(fileName)
                        ? AppSettings.ConfigurationProvider.DefaultGalleryImage
                        : $"{AppSettings.ConfigurationProvider.DirectoryGalleryImage}/{fileName}";
                    break;
                case FilePath.Logo:
                    fullPath = $"{AppSettings.ConfigurationProvider.DirectoryLogo}/{fileName}";
                    break;
                case FilePath.ProfileThumbnail:
                    fullPath = string.IsNullOrWhiteSpace(fileName)
                        ? AppSettings.ConfigurationProvider.DefaultProfileThumbnail
                        : $"{AppSettings.ConfigurationProvider.DirectoryProfileThumbnail}/{fileName}";
                    break;
                case FilePath.ProfileImage:
                    fullPath = string.IsNullOrWhiteSpace(fileName)
                        ? AppSettings.ConfigurationProvider.DefaultProfileImage
                        : $"{AppSettings.ConfigurationProvider.DirectoryProfileImage}/{fileName}";
                    break;
            }

            return fullPath;
        }

        public static string GetNewsFeaturedImagePath(string fileName)
        {
            var imgPath = GetFilePath(fileName, FilePath.News);
            return imgPath;
        }

        public static string GetImagePath(string fileName)
        {
            var imgPath = GetFilePath(fileName, FilePath.NewsImage);
            return imgPath;
        }

       

        public static string GetThumbnailPath(string fileName)
        {
            var imgPath = GetFilePath(fileName, FilePath.NewsThumbnail);
            return imgPath;
        }


        public static string UploadGallery(HttpPostedFileBase image, int thumbWidth)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "File can not be null.");

            var extension = Path.GetExtension(image.FileName);
            var imageName = $"Gallery_{Guid.NewGuid()}{ extension }";
            Image img = Image.FromStream(image.InputStream);
            Image thumb = img;

            if (img.Width > thumbWidth)
            {
                var thumbHeight = thumbWidth * img.Height / img.Width;
                thumb = ResizeImage(img, thumbWidth, thumbHeight);
            }

            var imgPath = HttpContext.Current.Server.MapPath(GetImagePath(imageName));
            var thumbPath = HttpContext.Current.Server.MapPath(GetThumbnailPath(imageName));

            while (File.Exists(imgPath))
            {
                imageName = $"Gallery_{Guid.NewGuid()}{ extension}";
                 imgPath = HttpContext.Current.Server.MapPath(GetImagePath(imageName));
                 thumbPath = HttpContext.Current.Server.MapPath(GetThumbnailPath(imageName));
            }

            img.Save(imgPath);
            thumb.Save(thumbPath);

            return imageName;
        }

        public static void UploadNewsImage(Image image, out string smallImageName, out string largeImageName)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "Image can not be null.");

           
            var smallImageWith = AppSettings.ConfigurationProvider.SmallNewsImageWidth;
            var largeImageWith = AppSettings.ConfigurationProvider.LargeNewsImageWidth;
            var subDirectory = $"{DateTime.Now.Year}/{DateTime.Now.Month}/";
            var guid = Guid.NewGuid();
            var extension = "jpg";
            largeImageName = $"{guid}_{largeImageWith}";
            smallImageName = $"{guid}_{smallImageWith}";

            var directory = AppSettings.ConfigurationProvider.DirectoryNewsImage;
            var directoryImage = HttpContext.Current.Server.MapPath($"{directory}/{subDirectory}");
            Directory.CreateDirectory(directoryImage);

            Image smallImage = image;

            if (image.Width > largeImageWith)
            {
                var height = largeImageWith * image.Height / image.Width;
                image = ResizeImage(image, largeImageWith, height);
            }

            if (smallImage.Width > smallImageWith)
            {
                var height = smallImageWith * image.Height / image.Width;
                smallImage = ResizeImage(image, smallImageWith, height);
            }

            while (File.Exists($"{directoryImage}{largeImageName}.{extension}") || File.Exists($"{directoryImage}{smallImageName}.{extension}"))
            {
                guid = Guid.NewGuid();
                largeImageName = $"{guid}_{largeImageWith}";
                smallImageName = $"{guid}_{smallImageWith}";
            }

            image.Save($"{directoryImage}{largeImageName}.{extension}");
            smallImage.Save($"{directoryImage}{smallImageName}.{extension}");

            largeImageName = $"{subDirectory}{largeImageName}.{extension}";
            smallImageName = $"{subDirectory}{smallImageName}.{extension}";
        }

        public static string UploadProfileImage(HttpPostedFileBase image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image), "File can not be null.");

            var extension = Path.GetExtension(image.FileName);
            var imageName = $"avatar_{Guid.NewGuid()}{extension}";

            Image img = Image.FromStream(image.InputStream);
            Image thumb = img;
            int smallWidth = AppSettings.ConfigurationProvider.ProfileThumbnailWidth,
                width = AppSettings.ConfigurationProvider.ProfileImageWidth;


            if (img.Width > width)
            {
                img = ResizeImage(img, width, width);
            }

            if (thumb.Width > smallWidth)
            {
                thumb = ResizeImage(thumb, smallWidth, smallWidth);
            }

            var imgPath = HttpContext.Current.Server.MapPath(GetFilePath(imageName, FilePath.ProfileImage));
            var thumbPath = HttpContext.Current.Server.MapPath(GetFilePath(imageName, FilePath.ProfileThumbnail));

            while (File.Exists(imgPath))
            {
                imageName = $"avatar_{Guid.NewGuid()}{ extension }";
                imgPath = HttpContext.Current.Server.MapPath(GetFilePath(imageName, FilePath.ProfileImage));
                thumbPath = HttpContext.Current.Server.MapPath(GetFilePath(imageName, FilePath.ProfileThumbnail));
            }

            img.Save(imgPath);
            thumb.Save(thumbPath);

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

        public static void DeleteProfileImage(string imageFileName, string thumbnailFileName)
        {
            // Delete image
            var fullImagePath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryProfileImage}/{imageFileName}");

            if (File.Exists(fullImagePath))
            {
                File.Delete(fullImagePath);
            }

            // Delete thumbnail
            var fullThumbnailPath = HttpContext.Current.Server.MapPath($"{AppSettings.ConfigurationProvider.DirectoryProfileThumbnail}/{thumbnailFileName}");
            if (File.Exists(fullThumbnailPath))
            {
                File.Delete(fullThumbnailPath);
            }
        }

        #endregion

        #region Logo
        public static string GetLogoPath(string fileName)
        {
            var imgPath = GetFilePath(fileName, FilePath.Logo);
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

        public static string GetNewsImageDirectory()
        {
            var imgPath = AppSettings.ConfigurationProvider.DirectoryNewsImage;
            return imgPath;
        }
        #endregion
    }
}
