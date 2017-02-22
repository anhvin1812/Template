using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.ProductManagement;
using App.Entities.ProductManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;

namespace App.Repositories.ProductManagement
{
    public class ProductCategoryRepository : RepositoryBase, IProductCategoryRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public ProductCategoryRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<ProductCategory> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<ProductCategory>();

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public IEnumerable<ProductCategory> GetByParentId(int? parentId)
        {
            var result = DatabaseContext.Get<ProductCategory>().Where(t=>t.ParentId == parentId);

            return result;
        }

        public ProductCategory GetById(int id)
        {
            return DatabaseContext.Get<ProductCategory>().FirstOrDefault(t=>t.Id == id);
        }

        public void Insert(ProductCategory entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(ProductCategory entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<ProductCategory>(id);
        }
    }
}
