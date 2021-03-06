﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Services.Dtos.Common;
using App.Services.Dtos.Gallery;

namespace App.Services.Dtos.ProductManagement
{
    public class ProductEntry : DtoBase
    {
        [Required(ErrorMessage = "Please enter product name.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [AllowHtml]
        public string Specifications { get; set; }

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
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [AllowHtml]
        public string Specifications { get; set; }
        public string Thumbnail { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }

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
        public string Specifications { get; set; }

        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public List<GallerySummary> Gallery { get; set; }

        public string Status { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
    }

}
