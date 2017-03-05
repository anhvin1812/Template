using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.SocialMediaTag
{
    public class ProductSocialMediaTag: DtoBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
    }
}
