using System.Collections.Generic;
using App.Services.Dtos.Gallery;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.ProductManagement;

namespace App.Services.NewsManagement
{
    public interface INewsService : IService
    {
        IEnumerable<NewsSummary> GetAll(int? page, int? pageSize, ref int? recordCount);
        NewsDetail GetById(int id);
        NewsUpdateEntry GetProductForEditing(int id);
        void Insert(NewsEntry entry);
        void Update(int id, NewsUpdateEntry entry);
        void Delete(int id);
    }
}
