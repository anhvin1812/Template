using System.Collections.Generic;
using App.Core.DataModels;

namespace App.Entities.NewsManagement
{
    public class NewsCategory : EntityBase, ICategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public bool? IsDisabled { get; set; }

        public virtual NewsCategory Parent { get; set; }

        public virtual ICollection<News> Newses { get; set; }
    }
}
