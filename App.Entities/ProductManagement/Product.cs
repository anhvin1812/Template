using System.Collections.Generic;
using App.Entities.ProductManagement;

namespace App.Entities.ProductManagement
{
    public class Product : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GalleryId { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }
        public virtual ProductStatus Status { get; set; }
        public virtual Gallery Image { get; set; }
        public virtual ICollection<Gallery> Gallery { get; set; }
    }
}
