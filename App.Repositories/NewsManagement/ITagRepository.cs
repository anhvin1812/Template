using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;

namespace App.Repositories.NewsManagement
{
    public interface ITagRepository : IRepository
    {
        IEnumerable<Tag> GetAll(int? page, int? pageSize, ref int? recordCount);
        IEnumerable<Tag> GetByIds(IEnumerable<int> ids);
        Tag GetById(int id);
        Tag GetByName(string name);
        void Insert(Tag entity);
        void Update(Tag entity);
        void Delete(int id);
    }
}
