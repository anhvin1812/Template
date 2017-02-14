using System.Collections.Generic;

namespace App.Entities.ProductManagement
{
    public class ProductGallery : EntityBase
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public string Thumbnail { get; set; }
    }
}
