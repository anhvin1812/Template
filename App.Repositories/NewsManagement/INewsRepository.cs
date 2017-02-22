using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;

namespace App.Repositories.NewsManagement
{
    public interface INewsRepository : IRepository
    {
        IEnumerable<News> GetAll(int? page, int? pageSize, ref int? recordCount);
        News GetById(int id);
        void Insert(News entity);
        void Update(News entity);
        void Delete(int id);
    }
}
