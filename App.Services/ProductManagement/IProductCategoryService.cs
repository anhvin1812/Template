using System.Collections.Generic;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.ProductManagement;

namespace App.Services.ProductManagement
{
    public interface IProductCategoryService : IService
    {
        IEnumerable<ProductCategoryDetail> GetAll(int? page, int? pageSize, ref int? recordCount);
        ProductCategoryDetail GetById(int id);
        void Insert(ProductCategoryEntry entry);
        void Update(int id, ProductCategoryEntry entry);

    }
}
