using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.NewsManagement
{
    public class NewsEntry : DtoBase
    {
        [Required(ErrorMessage = "Please enter title.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public bool IsHot { get; set; }
        public bool IsFeatured { get; set; }
        [Required(ErrorMessage = "Please select status.")]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Please select media type.")]
        public int MediaTypeId { get; set; }

        [Required(ErrorMessage = "Please select categories.")]
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }

        [Required(ErrorMessage = "Please upload featured image.")]
        public HttpPostedFileBase Image { get; set; }
    }

    public class NewsUpdateEntry : DtoBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter title.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public bool IsHot { get; set; }
        public bool IsFeatured { get; set; }
        [Required(ErrorMessage = "Please select status.")]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Please select media type.")]
        public int MediaTypeId { get; set; }

        [Required(ErrorMessage = "Please select categories.")]
        public List<int> CategoryIds { get; set; }
        public List<int> TagIds { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }

    public class NewsSummary : DtoBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Views { get; set; }
        public string Status { get; set; }
        public bool IsHot { get; set; }
        public bool IsFeatured { get; set; }
        public int MediaTypeId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }
    }

    public class NewsDetail : DtoBase
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public bool IsHot { get; set; }
        public bool IsFeatured { get; set; }
        public int Views { get; set; }
        public int StatusId { get; set; }
        public int MediaTypeId { get; set; }
        public List<NewsCategorySummary> Categories { get; set; }
        public List<TagSummary> Tags { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }


    public class StatusSummary
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public class NewsFilter
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
        public int? StatusId { get; set; }
        public int? MediaTypeId { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsFeatured { get; set; }
    }

}
