using System.ComponentModel.DataAnnotations;
using System.Web;
using App.Services.Dtos.Common;
using App.Core.News;
using System.Collections.Generic;
using System.Web.Mvc;
using App.Services.Dtos.NewsManagement;

namespace App.Services.Dtos.Settings
{
    public class OptionDetail : DtoBase
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Logo { get; set; }
    }

    public class OptionEntry : DtoBase
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Website { get; set; }

        [AllowHtml]
        public string Facebook { get; set; }

        public string LogoFileName { get; set; }
        public HttpPostedFileBase Logo { get; set; }
    }

    public class SettingDetail : DtoBase
    {
        public string Menu { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Logo { get; set; }
    }


    public class HomepageLayOutDetail : DtoBase
    {
        public int Id { get; set; }
        public int? CategoryId{ get; set; }
        public string Title { get; set; }
        public MediaType? MediaType { get; set; }
        public LayoutType? LayoutType { get; set; }
        public int? SortOrder { get; set; }
    }

    public class SettingHomepageViewModel : DtoBase
    {
        public IEnumerable<NewsCategorySummary> Categories { get; set; }
        public IEnumerable<HomepageLayOutDetail> HomepageLayOuts { get; set; }
    }

    public class HomepageLayoutEntry : DtoBase
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public MediaType? MediaType { get; set; }
        public LayoutType? LayoutType { get; set; }
        public int SortOrder { get; set; }
    }

    public class HomepageViewModel : DtoBase
    {
        public IEnumerable<HomepageLayOutDetail> Layouts { get; set; }
    }

    public class LayoutViewModel : DtoBase
    {
        public SettingDetail Setting { get; set; }
        public NewsDetail SocialMetaTags { get; set; }
    }
}
