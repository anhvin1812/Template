using System;
using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;

namespace App.Repositories.NewsManagement
{
    public interface INewsRepository : IRepository
    {
        IEnumerable<News> GetAll(string keyword, int? categoryId, int? statusId, int? mediaTypeId, bool? hot, bool? featured, int? page, int? pageSize, ref int? recordCount);
        News GetById(int id);
        void Insert(News entity);
        void Update(News entity);
        void Delete(int id);

        #region Status

        IEnumerable<NewsStatus> GetAllStatus();
        NewsStatus GetStatusById(int id);
        #endregion

        #region Public news
        News GetPublicNewsById(int id);

        IEnumerable<News> GetFeaturedNews(int? maxRecords = 5);

        IEnumerable<News> GetHotNews(int? maxRecords = 10);

        IEnumerable<News> GetLatestNews(int categoryId, int? maxRecords = 5);

        IEnumerable<News> GetRelatedNews(int newsId, int categoryId, int? maxRecords = 5);

        IEnumerable<News> GetMostViews(DateTime? startDate = null, DateTime? endDate = null, int? maxRecords = 5);

        IEnumerable<News> GetAllPublicNews(string keyword, DateTime? startDate, DateTime? endDate, int? categoryId, int? tagId, int? page, int? pageSize, ref int? recordCount);

        #endregion
    }
}
