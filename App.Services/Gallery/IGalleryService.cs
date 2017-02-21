using System.Collections.Generic;
using App.Services.Dtos.Gallery;
using App.Services.Dtos.ProductManagement;
using App.Services.Dtos.UI;

namespace App.Services.Gallery
{
    public interface IGalleryService : IService
    {
        void Delete(int id);
    }
}
