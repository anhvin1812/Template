using System.Web.Mvc;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.Settings;
using App.Services.Settings;

namespace App.Website.Fillters
{
    public class LayoutActionFilter : ActionFilterAttribute
    {
        public ISettingService SettingService { set; get; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var settings = SettingService.GetSetting();
            var layoutViewModel = new LayoutViewModel { Setting = settings };

            // get social meta tags data
            var socialMetaTags = (NewsDetail)filterContext.Controller.ViewBag.SocialMetaTags;
            layoutViewModel.SocialMetaTags = socialMetaTags;

            filterContext.Controller.ViewBag.LayoutViewModel = layoutViewModel;
        }
    }
}