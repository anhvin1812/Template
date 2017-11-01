using System.Collections.Generic;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;

namespace App.Services.NewsManagement
{
    public interface ICrawlService : IService
    {
        IEnumerable<CrawlSummary> Scan(CrawlFilter filter);
        void Save(CrawlEntry entry);
        PageArticleDetail GetArticleDetail(int sourceId, string linkArticleDetail);

        #region Pages

        IEnumerable<PageSummary> GetPagesBySourceId(int sourceId);

        #endregion

        #region Dropdown list
        SelectListOptions GetCrawlSourceOptionsForDropdownList();
        SelectListOptions GetCrawlSourcePageOptionsForDropdownList(int crawlSourceId);
        #endregion

    }
}
