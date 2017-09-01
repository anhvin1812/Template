using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;

namespace App.Repositories.NewsManagement
{
    public interface ITagRepository : IRepository
    {
        IEnumerable<Tag> GetAll(string keyword, bool? isDisabled, int? page, int? pageSize, ref int? recordCount);
        IEnumerable<Tag> GetByIds(IEnumerable<int> ids);
        IEnumerable<Tag> GetMostUsedTags(bool? isDisabled = null, int maxRecords = 10);
        Tag GetById(int id);
        Tag GetByName(string name);
        void Insert(Tag entity);
        void Update(Tag entity);
        void Delete(int id);
    }
}
