using System.Collections.Generic;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;

namespace App.Services.NewsManagement
{
    public interface ITagService : IService
    {
        IEnumerable<TagSummary> GetAll(string keyword, bool? isDisabled, int? page, int? pageSize, ref int? recordCount);
        IEnumerable<TagSummary> GetByStringIds(string ids, bool? isDisabled = null);
        IEnumerable<TagSummary> GetMostUsedTags(bool? isDisabled = null, int maxRecords = 10);
        SelectListOptions GetOptionsForDropdownList(List<int> selectedIds, bool? isDisabled = null);
        TagDetail GetById(int id);

        TagEntry GetEntryForEditing(int id);

        void Insert(TagEntry entry);
        void Update(int id, TagEntry entry);

    }
}
