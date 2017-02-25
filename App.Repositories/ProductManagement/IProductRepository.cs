using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.ProductManagement;

namespace App.Repositories.ProductManagement
{
    public interface IProductRepository : IRepository
    {
        IEnumerable<Product> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount);
        IEnumerable<Product> GetRelatedProducts(int productId, int categoryId, int? maxRecords = null);
        Product GetById(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int id);

        #region Product Status

        IEnumerable<ProductStatus> GetAllStatus();

        #endregion
    }
}
