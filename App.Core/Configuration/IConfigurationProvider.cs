﻿namespace App.Core.Configuration
{
    public interface IConfigurationProvider
    {
        string DirectoryProductImage { get; }
        string DirectoryProductThumbnail { get; }
        string DirectoryGalleryImage { get; }
        string DirectoryGalleryThumbnail { get; }
        string DefaultGalleryThumbnail { get; }
        string DefaultGalleryImage { get; }
       
    }
}
