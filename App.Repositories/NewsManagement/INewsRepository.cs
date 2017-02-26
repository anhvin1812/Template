using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;

namespace App.Repositories.NewsManagement
{
    public interface INewsRepository : IRepository
    {
        IEnumerable<News> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount);
        IEnumerable<News> GetRelatedNews(int newsId, int categoryId, int? maxRecords = null);
        News GetById(int id);
        void Insert(News entity);
        void Update(News entity);
        void Delete(int id);
    }
}
