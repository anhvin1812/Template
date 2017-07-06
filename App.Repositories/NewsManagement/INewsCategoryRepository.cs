using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;

namespace App.Repositories.NewsManagement
{
    public interface INewsCategoryRepository : IRepository
    {
        IEnumerable<NewsCategory> GetAll(int? page, int? pageSize, ref int? recordCount);
        IEnumerable<NewsCategory> GetByParentId(int? parentId);
        IEnumerable<NewsCategory> GetByIds(IEnumerable<int> ids);
        NewsCategory GetById(int id);
        void Insert(NewsCategory product);
        void Update(NewsCategory product);
        void Delete(int id);
    }
}
