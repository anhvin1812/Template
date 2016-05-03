using System.Web.Mvc;
using System.Web.Routing;

namespace App.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Get attribute routes to work
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 new[] { "App.Website.Controllers" }
            );
        }
    }
}
