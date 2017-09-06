using System.Collections.Generic;

namespace App.Services.Gallery
{
    public interface IGalleryService : IService
    {
        void Delete(int id);
    }
}
