using System.Collections.Generic;
using App.Core.DataModels;

namespace App.Entities.NewsManagement
{
    public class Tag : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool? IsDisabled { get; set; }

        public virtual ICollection<News> Newses { get; set; }
    }
}
