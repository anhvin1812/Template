using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Repositories.Common;

namespace App.Repositories.NewsManagement
{
    public class CrawlRepository : RepositoryBase, ICrawlRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public CrawlRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #region CrawlSource

        public IEnumerable<CrawlSource> GetCrawlSources(bool? isDisabled, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<CrawlSource>();

            if (isDisabled.HasValue)
            {
                result = isDisabled == true ? result.Where(t => t.IsDisabled == true) : result.Where(t => t.IsDisabled != true);
            }

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.OrderBy(t => t.Name).ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public CrawlSource GetCrawlSourceById(int crawlSourceId)
        {
            return DatabaseContext.FindById<CrawlSource>(crawlSourceId);
        }
        #endregion

        #region CrawlSourcePage

        public IEnumerable<CrawlSourcePage> GetCrawlSourcePageBySourceId(int crawlSourceId)
        {
            var results = DatabaseContext.Get<CrawlSourcePage>().Where(t=>t.CrawlSourceId == crawlSourceId);
            return results;
        }

        public CrawlSourcePage GetCrawlSourcePageById(int crawlSourcePageId)
        {
            return DatabaseContext.FindById<CrawlSourcePage>(crawlSourcePageId);
        }

        public IEnumerable<CrawlSourcePage> GetCrawlSourcePageByIds(List<int> crawlSourcePageIds)
        {
            var results =  DatabaseContext.Get<CrawlSourcePage>().Where(t=> crawlSourcePageIds.Contains(t.Id));
            return results;
        }
        #endregion

        #region CrawlSourcePageDetail

        public CrawlSourcePageDetail GetCrawlSourcePageDetailById(int crawlSourcePageDetailId)
        {
            return DatabaseContext.FindById<CrawlSourcePageDetail>(crawlSourcePageDetailId);
        }

        public CrawlSourcePageDetail GetCrawlSourcePageDetailBySourceId(int crawlSourceId)
        {
            var result = DatabaseContext.Get<CrawlSourcePageDetail>().FirstOrDefault(t => t.CrawlSourceId == crawlSourceId);
            return result;
        }
        #endregion

        #region CrawlArticleSection

        public IEnumerable<CrawlArticleSection> GetCrawlArticleSectionBySourcePageId(int crawlSourcePageId)
        {
            var results = DatabaseContext.Get<CrawlArticleSection>().Where(t=>t.CrawlSourcePageId == crawlSourcePageId);
            return results;
        }

        public CrawlArticleSection GetCrawlArticleSectionById(int crawlArticleSectionId)
        {
            return DatabaseContext.FindById<CrawlArticleSection>(crawlArticleSectionId);
        }
        #endregion
    }
}
