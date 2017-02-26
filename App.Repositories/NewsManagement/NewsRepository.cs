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

        public IEnumerable<News> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<News>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result = result.Where(t => t.Title.Contains(keyword) || t.Description.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                var lstHereditaryIds = DatabaseContext.Get<NewsCategory>().GetHereditaryIds(categoryId.Value);

                result = result.Where(t => lstHereditaryIds.Contains(t.CategoryId));
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

            var lstHereditaryIds = DatabaseContext.Get<NewsCategory>().GetHereditaryIds(categoryId);
            result = result.Where(t => lstHereditaryIds.Contains(t.CategoryId));

            if (maxRecords.HasValue)
            {
                result = result.Take(maxRecords.Value);
            }

            return result;
        }

        public News GetById(int id)
        {
            return DatabaseContext.Get<News>().FirstOrDefault(t=>t.Id == id);
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
    }
}
