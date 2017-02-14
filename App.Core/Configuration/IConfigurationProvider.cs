using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sherpa.Core
{
    public interface IConfigurationProvider
    {
        string DirectoryProductImage { get; }
        string DirectoryProductThumbnail { get; }
        string DirectoryGalleryImage { get; }
        string DirectoryGalleryThumbnail { get; }
       
    }
}
