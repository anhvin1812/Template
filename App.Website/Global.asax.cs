using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using App.Core.Exceptions;
using App.Infrastructure.IdentityManagement;
using App.Website.App_Start;
using App.Website.Controllers;
using App.Website.ModelBinder;

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

            ModelBinders.Binders.DefaultBinder = new AppModelBinder();

            //ExceptionHandlingConfig.RegisterExceptionHandler(GlobalConfiguration.Configuration);

            //Migrations.Initialize();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            var ex = Server.GetLastError();

            if (ex is DataNotFoundException)
            {
                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 404;
            }

            //httpContext.ClearError();
            //httpContext.Response.Clear();
            //httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            //httpContext.Response.TrySkipIisCustomErrors = true;

            //var routeData = new RouteData();
            //routeData.Values["controller"] = "ControllerName";
            //routeData.Values["action"] = "ActionName";
            //routeData.Values["error"] = "404"; //Handle this url paramater in your action
            //((IController)new AccountController()).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }
    }
}
