using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.IdentityManagement;
using App.Entities.ProductManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;

namespace App.Repositories.ProductManagement
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public ProductRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<Product> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<Product>();

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

        public Product GetById(int id)
        {
            return DatabaseContext.Get<Product>().FirstOrDefault(t=>t.Id == id);
        }

        public void Insert(Product entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(Product entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<Product>(id);
        }
    }
}
