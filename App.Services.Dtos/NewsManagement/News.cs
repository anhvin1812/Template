using System;
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

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }

    public class NewsUpdateEntry : DtoBase
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }

    public class NewsSummary : DtoBase
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class NewsDetail : DtoBase
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

}
