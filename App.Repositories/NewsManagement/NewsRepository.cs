using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;
using App.Repositories.Common;

namespace App.Repositories.NewsManagement
{
    public class NewsRepository : RepositoryBase, INewsRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public NewsRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<News> GetAll(string keyword, int? categoryId, int? statusId, bool? hot, bool? featured, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<News>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result = result.Where(t => t.Title.Contains(keyword) || t.Description.Contains(keyword));
            }

            //get by category
            if (categoryId.HasValue)
            {
                result = result.Where(t => t.Categories.Any(c=>c.Id == categoryId));
            }
            
            //get by status
            if (statusId.HasValue)
            {
                result = result.Where(t => t.StatusId == statusId);
            }

            //get hot news
            if (hot.HasValue)
            {
                result = result.Where(t => t.IsHot == hot.Value);
            }

            //get featured news
            if (featured.HasValue)
            {
                result = result.Where(t => t.IsFeatured == featured.Value);
            }

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.OrderByDescending(t => t.Id).ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public IEnumerable<News> GetRelatedNews(int newsId, int categoryId, int? maxRecords = null)
        {
            var result = DatabaseContext.Get<News>().Where(t => t.Id != newsId);

            result = result.Where(t => t.Categories.Any(c => c.Id == categoryId));

            if (maxRecords.HasValue)
            {
                result = result.Take(maxRecords.Value);
            }

            return result;
        }

        public News GetById(int id)
        {
            return DatabaseContext.FindById<News>(id);
        }

        public void Insert(News entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(News entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<News>(id);
        }

        #region Status

        public IEnumerable<NewsStatus> GetAllStatus()
        {
            return DatabaseContext.Get<NewsStatus>();
        }

        public NewsStatus GetStatusById(int id)
        {
            return DatabaseContext.FindById<NewsStatus>(id);
        }

        #endregion
    }
}
