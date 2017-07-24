using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Repositories;
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

            filterContext.Controller.ViewBag.LayoutViewModel = layoutViewModel;
        }
    }
}