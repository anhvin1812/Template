using System;
using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.ProductManagement;
using App.Repositories.Common;

namespace App.Repositories.ProductManagement
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public ProductRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<Product> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<Product>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result = result.Where(t => t.Name.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                var lstHereditaryIds = DatabaseContext.Get<ProductCategory>().GetHereditaryIds(categoryId.Value);

                result = result.Where(t=>lstHereditaryIds.Contains(t.CategoryId));
            }

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.OrderByDescending(t=>t.Id).ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public IEnumerable<Product> GetRelatedProducts(int productId, int categoryId, int? maxRecords = null)
        {
            var result = DatabaseContext.Get<Product>().Where(t => t.Id != productId);

            var lstHereditaryIds = DatabaseContext.Get<ProductCategory>().GetHereditaryIds(categoryId);
            result = result.Where(t => lstHereditaryIds.Contains(t.CategoryId));

            if (maxRecords.HasValue)
            {
                result = result.Take(maxRecords.Value);
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

        #region Product Status

        public IEnumerable<ProductStatus> GetAllStatus()
        {
            return DatabaseContext.Get<ProductStatus>();
        }

        #endregion
    }
}
