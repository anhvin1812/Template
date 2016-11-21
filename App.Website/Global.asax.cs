using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App.Infrastructure.IdentityManagement;
using App.Website.App_Start;

namespace App.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebAutofacConfig.ConfigureContainer();

            ExceptionHandlingConfig.RegisterExceptionHandler(GlobalConfiguration.Configuration);

            //Migrations.Initialize();
        }
    }
}
