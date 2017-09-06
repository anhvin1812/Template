using System.Collections.Generic;

namespace App.Entities.FileManagement
{
    public class Gallery : EntityBase
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public string Thumbnail { get; set; }
    }
}
