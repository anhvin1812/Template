using System.Collections.Generic;
using App.Services.Dtos.ProductManagement;

namespace App.Services.ProductManagement
{
    public interface IProductService : IService
    {
        IEnumerable<ProductSummary> GetAll(int? page, int? pageSize, ref int? recordCount);
        ProductDetail GetById(int id);
        void Insert(ProductEntry entry);
        void Update(int id, ProductUpdateEntry entry);

        #region Product Status
        IEnumerable<ProductStatusSummary> GetAllStatus();
        #endregion

    }
}
