﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using App.Core.DataModels;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.ProductManagement
{
    public class ProductCategoryEntry : DtoBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool? IsDisabled { get; set; }
    }

    public class ProductCategoryDetail : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsDisabled { get; set; }

        public ProductCategoryDetail Parent { get; set; }
    }

    public class ProductCategorySummary : DtoBase, ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

    }
}
