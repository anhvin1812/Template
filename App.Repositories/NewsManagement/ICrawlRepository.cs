using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;

namespace App.Repositories.NewsManagement
{
    public interface ICrawlRepository : IRepository
    {
        #region CrawlSource
        IEnumerable<CrawlSource> GetCrawlSources(bool? isDisabled, int? page, int? pageSize, ref int? recordCount);
        CrawlSource GetCrawlSourceById(int crawlSourceId);
        #endregion

        #region CrawlSourcePage
        IEnumerable<CrawlSourcePage> GetCrawlSourcePageBySourceId(int crawlSourceId);
        CrawlSourcePage GetCrawlSourcePageById(int crawlSourcePageId);

        IEnumerable<CrawlSourcePage> GetCrawlSourcePageByIds(List<int> crawlSourcePageIds);
        #endregion

        #region CrawlSourcePageDetail
        CrawlSourcePageDetail GetCrawlSourcePageDetailById(int crawlSourcePageDetailId);
        CrawlSourcePageDetail GetCrawlSourcePageDetailBySourceId(int crawlSourceId);
        #endregion

        #region CrawlArticleSection
        IEnumerable<CrawlArticleSection> GetCrawlArticleSectionBySourcePageId(int crawlSourcePageId);

        CrawlArticleSection GetCrawlArticleSectionById(int crawlArticleSectionId);
        #endregion
    }
}
