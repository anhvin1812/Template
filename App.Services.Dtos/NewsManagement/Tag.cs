using System.ComponentModel.DataAnnotations;
using App.Core.DataModels;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.NewsManagement
{
    public class TagEntry : DtoBase
    {
        [Required]
        public string Name { get; set; }
        public bool? IsDisabled { get; set; }
    }

    public class TagDetail : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public bool IsDisabled { get; set; }
    }

    public class TagSummary : DtoBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDisabled { get; set; }
    }
}
