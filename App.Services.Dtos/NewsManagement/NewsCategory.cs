using System.ComponentModel.DataAnnotations;
using App.Core.DataModels;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.NewsManagement
{
    public class NewsCategoryEntry : DtoBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool? IsDisabled { get; set; }
    }

    public class NewsCategoryDetail : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsDisabled { get; set; }

        public NewsCategoryDetail Parent { get; set; }
    }

    public class NewsCategorySummary : DtoBase, ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public int NewsCount { get; set; }

    }
}
