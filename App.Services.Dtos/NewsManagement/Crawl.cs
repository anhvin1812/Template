
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using App.Services.Dtos.Common;
using PagedList;

namespace App.Services.Dtos.NewsManagement
{

    public class CrawlFilter : DtoBase
    {
        public int CrawlSourceId { get; set; }
        public List<int> CrawlSourcePageIds { get; set; }
        public string Keyword { get; set; }
    }

    public class CrawlSummary : DtoBase
    {
        public int CrawlSourceId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Desciption { get; set; }
        public string Content { get; set; }
        public string FeaturedImage { get; set; }
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Categories { get; set; }
    }

    public class PageArticleDetail : DtoBase
    {
        public PageArticleDetail()
        {
            Tags = new List<string>();
            Categories = new List<string>();
            Date = DateTime.Now;
        }

        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Categories { get; set; }
    }

    public class CrawlEntry : DtoBase
    {
        [Required(ErrorMessage = "Please enter title.")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "The length of description cannot be over than 500 characters.")]
        public string Description { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string FeaturedImage { get; set; }
        public bool IsHot { get; set; }
        public bool IsFeatured { get; set; }
        [Required(ErrorMessage = "Please select status.")]
        public int MediaTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Please select category.")]
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Categories { get; set; }
    }

    #region Pages
    public class PageSummary : DtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    #endregion
}
