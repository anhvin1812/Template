using System;
using System.Collections.Generic;
using App.Entities.NewsManagement;
using App.Services.Dtos.Gallery;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;

namespace App.Services.NewsManagement
{
    public interface IPublicNewsService : IService
    {
        IEnumerable<PublicNewsSummary> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount);

        PublicNewsDetail GetPublicNewsById(int id);

        IEnumerable<PublicNewsSummary> GetFeaturedNews(int? maxRecords = 5);

        IEnumerable<PublicNewsSummary> GetHotNews(int? maxRecords = 10);

        IEnumerable<PublicNewsSummary> GetLatestNews(int categoryId, int? maxRecords = 5);

        IEnumerable<PublicNewsSummary> GetRelatedNews(int newsId, int categoryId, int? maxRecords = 5);

        IEnumerable<PublicNewsSummary> GetMostViews(DateTime? startDate = null, DateTime? endDate = null, int? maxRecords = 5);

    }
}
