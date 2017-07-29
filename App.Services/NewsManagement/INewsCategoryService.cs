using System.Collections.Generic;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;

namespace App.Services.NewsManagement
{
    public interface INewsCategoryService : IService
    {
        IEnumerable<NewsCategorySummary> GetAll(bool? isDisabled, int? page, int? pageSize, ref int? recordCount);
        SelectListOptions GetOptionsForDropdownList(int? parentId, int? currentId = null, bool? isDisabled = null);
        NewsCategoryDetail GetById(int id);
        NewsCategoryEntry GetCategoryForEditing(int id);

        void Insert(NewsCategoryEntry entry);
        void Update(int id, NewsCategoryEntry entry);

    }
}
