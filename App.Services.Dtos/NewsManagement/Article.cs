using System;
using System.Collections.Generic;
using App.Services.Dtos.Common;
using PagedList;

namespace App.Services.Dtos.NewsManagement
{

    public class ArticleSearch : DtoBase
    {
        public StaticPagedList<PublicNewsSummary> PagedNews { get; set; }
        public NewsFilter NewsFilter { get; set; }
    }

    public class ArticleCategory : DtoBase
    {
        public StaticPagedList<PublicNewsSummary> PagedNews { get; set; }
        public IEnumerable<NewsCategorySummary> Categories { get; set; }
    }
}
