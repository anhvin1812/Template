using App.Core.DataModels;
using App.Entities.NewsManagement;

namespace App.Entities.Settings
{
    public class HomepageLayout : EntityBase
    {
        public int Id { get; set; }

        public int? CategoryId { get; set; }
        public int? MediaTypeId { get; set; }
        public int? LayoutTypeId { get; set; }
        public int? SortOrder { get; set; }

        public virtual NewsCategory Category { get; set; }
    }
}
