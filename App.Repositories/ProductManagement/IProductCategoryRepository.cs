using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.ProductManagement;

namespace App.Repositories.ProductManagement
{
    public interface IProductCategoryRepository : IRepository
    {
        IEnumerable<ProductCategory> GetAll(int? page, int? pageSize, ref int? recordCount);
        ProductCategory GetById(int id);
        void Insert(ProductCategory product);
        void Update(ProductCategory product);
        void Delete(int id);
    }
}
