using System.Web.Mvc;
using System.Web.Optimization;
using App.Website.Areas.Admin.App_Start;

namespace App.Website.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterRoutes(context);
            RegisterBundles();
        }

        private void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "App.Website.Areas.Admin.Controllers" }
            );

            context.MapRoute(
            "Admin_elmah",
            "Admin/elmah/{type}",
            new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
        );
        }

        private void RegisterBundles()
        {
            AdminBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}