using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.FileManagement;

namespace App.Repositories.ProductManagement
{
    public class GalleryRepository : RepositoryBase, IGalleryRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public GalleryRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        //public IEnumerable<Gallery> GetByProductId(int id)
        //{
        //    var result = DatabaseContext.Get<Gallery>().Where(t=>t.);

        //    if (recordCount != null)
        //    {
        //        recordCount = result.Count();
        //    }

        //    if (page != null && pageSize != null)
        //    {
        //        result = result.ApplyPaging(page.Value, pageSize.Value);
        //    }

        //    return result;
        //}

        public Gallery GetById(int id)
        {
            return DatabaseContext.Get<Gallery>().FirstOrDefault(t => t.Id == id);
        }

        public void Insert(Gallery entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(Gallery entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<Gallery>(id);
        }

        //#region Product Status

        //public IEnumerable<ProductStatus> GetAllStatus()
        //{
        //    return DatabaseContext.Get<ProductStatus>();
        //}

        //#endregion
    }
}
