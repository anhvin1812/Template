using System.Collections.Generic;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.Dtos.UI;

namespace App.Services.ProductManagement
{
    public interface IProductCategoryService : IService
    {
        IEnumerable<ProductCategorySummary> GetAll(int? page, int? pageSize, ref int? recordCount);
        SelectListOptions GetOptionsForDropdownList(int? parentId, int? currentId = null, bool? isDisabled = null);
        ProductCategoryDetail GetById(int id);
        ProductCategoryEntry GetCategoryForEditing(int id);

        void Insert(ProductCategoryEntry entry);
        void Update(int id, ProductCategoryEntry entry);

    }
}
