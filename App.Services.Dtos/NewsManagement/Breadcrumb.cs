using System;
using System.Collections.Generic;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.NewsManagement
{
    public class Breadcrumb : DtoBase
    {
        public string Title { get; set; }
        public IEnumerable<NewsCategorySummary> Categories { get; set; }
        public TagSummary Tag { get; set; }
    }
}
