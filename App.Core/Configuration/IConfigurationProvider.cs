﻿using System.Net.Configuration;

namespace App.Core.Configuration
{
    public interface IConfigurationProvider
    {
        string DirectoryProductImage { get; }
        string DirectoryProductThumbnail { get; }
        string DirectoryGalleryImage { get; }
        string DirectoryGalleryThumbnail { get; }
        string DefaultGalleryThumbnail { get; }
        string DefaultGalleryImage { get; }
        string DirectoryNewsImage { get; }

        int SmallNewsImageWidth { get; }
        int LargeNewsImageWidth { get; }

        string DirectoryLogo { get; }

        string DirectoryProfileImage { get; }
        string DirectoryProfileThumbnail { get; }
        string DefaultProfileImage { get; }
        string DefaultProfileThumbnail { get; }


        SmtpSection SmtpConfiguration { get; }
    }
}
