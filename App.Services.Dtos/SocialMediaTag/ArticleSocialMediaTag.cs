using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.SocialMediaTag
{
    public class ArticleSocialMediaTag : DtoBase
    {
        public ArticleSocialMediaTag()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string LargeImage { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Creator { get; set; }
    }
}
