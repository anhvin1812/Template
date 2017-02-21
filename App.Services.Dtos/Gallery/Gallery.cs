using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.Gallery
{
    public class GallerySummary : DtoBase
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
    }

    public class GalleryDetail : DtoBase
    {
        public int Id { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
    }

    public class GalleryEntry : DtoBase
    {
        public HttpPostedFileBase Image { get; set; }
    }

}
