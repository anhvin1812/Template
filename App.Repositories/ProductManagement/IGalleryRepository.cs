using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.FileManagement;

namespace App.Repositories.ProductManagement
{
    public interface IGalleryRepository : IRepository
    {
        Gallery GetById(int id);
        void Insert(Gallery entity);
        void Update(Gallery entity);
        void Delete(int id);
    }
}
