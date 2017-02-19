using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.ProductManagement
{
    public class ProductEntry : DtoBase
    {
        [Required(ErrorMessage = "Please enter product name.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [AllowHtml]
        public string Specification { get; set; }

        [Required(ErrorMessage = "Please enter price.")]
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }

        [Required(ErrorMessage = "Please select a status.")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public List<HttpPostedFileBase> Gallery { get; set; }
    }

    public class ProductUpdateEntry : DtoBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public List<HttpPostedFileBase> Gallery { get; set; }
    }

    public class ProductSummary : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Status { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
    }

    public class ProductDetail : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }

        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public List<string> GalleryThumbnails { get; set; }
        public List<string> Gallery { get; set; }

        public string Status { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
    }

}
