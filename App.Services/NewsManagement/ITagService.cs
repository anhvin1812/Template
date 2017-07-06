using System.Collections.Generic;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;

namespace App.Services.NewsManagement
{
    public interface ITagService : IService
    {
        IEnumerable<TagSummary> GetAll(int? page, int? pageSize, ref int? recordCount);
        SelectListOptions GetOptionsForDropdownList(bool? isDisabled = null);
        TagDetail GetById(int id);

        TagEntry GetEntryForEditing(int id);

        void Insert(TagEntry entry);
        void Update(int id, TagEntry entry);

    }
}
