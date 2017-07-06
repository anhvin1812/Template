using System;
using System.Collections;
using System.Collections.Generic;
using App.Entities.ProductManagement;

namespace App.Entities.NewsManagement
{
    public class News : EntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GalleryId { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsDisabled { get; set; }
        public int Views { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int UpdatedById { get; set; }

        public virtual ICollection<NewsCategory> Categories { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual Gallery Image { get; set; }
        public virtual User Editor { get; set; }

    }
}
