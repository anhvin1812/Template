using System.Web.Mvc;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.Settings;
using App.Services.Settings;
using App.Services.NewsManagement;

namespace App.Website.Fillters
{
    public class LayoutActionFilter : ActionFilterAttribute
    {
        public ISettingService SettingService { set; get; }
        public ITagService TagService { set; get; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var settings = SettingService.GetSetting();
            var tags = TagService.GetMostUsedTags(false);

            var layoutViewModel = new LayoutViewModel
            {
                Setting = settings,
                MostUsedTags = tags
            };

            // get social meta tags data
            var socialMetaTags = (PublicNewsDetail)filterContext.Controller.ViewBag.SocialMetaTags;
            layoutViewModel.SocialMetaTags = socialMetaTags;

            filterContext.Controller.ViewBag.LayoutViewModel = layoutViewModel;
        }
    }
}