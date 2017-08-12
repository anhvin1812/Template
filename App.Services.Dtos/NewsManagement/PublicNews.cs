using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.NewsManagement
{

    public class PublicNewsSummary : DtoBase
    {
        public PublicNewsSummary()
        {
            Category = new PublicCategorySummary();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public int Views { get; set; }

        public DateTime PublishedDate { get; set; }
        public string UpdatedBy { get; set; }

        public PublicCategorySummary Category { get; set; }
    }

    public class PublicNewsDetail : DtoBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public int Views { get; set; }
        public int MediaTypeId { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }

        public List<PublicCategorySummary> Categories { get; set; }
        public List<PublicTagSummary> Tags { get; set; }
    }

    public class PublicTagSummary : DtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PublicCategorySummary : DtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LatestNews : DtoBase
    {
        public PublicCategorySummary Category { get; set; }
        public IEnumerable<PublicNewsSummary> News { get; set; }
    }

    public class TopViewSidebar : DtoBase
    {
        public IEnumerable<PublicNewsSummary> Weekly { get; set; }
        public IEnumerable<PublicNewsSummary> Monthly { get; set; }
        public IEnumerable<PublicNewsSummary> All { get; set; }
    }

}
