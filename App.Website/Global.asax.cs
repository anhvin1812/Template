using System;
using System.Globalization;
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

            //System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            //ExceptionHandlingConfig.RegisterExceptionHandler(GlobalConfiguration.Configuration);

            //Migrations.Initialize();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //var httpContext = ((MvcApplication)sender).Context;
            //var currentController = " ";
            //var currentAction = " ";
            //var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            //if (currentRouteData != null)
            //{
            //    if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
            //    {
            //        currentController = currentRouteData.Values["controller"].ToString();
            //    }

            //    if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
            //    {
            //        currentAction = currentRouteData.Values["action"].ToString();
            //    }
            //}

            //var ex = Server.GetLastError();
            //var controller = new ErrorController();
            //var routeData = new RouteData();
            //var action = "Index";

            //if (ex is HttpException)
            //{
            //    var httpEx = ex as HttpException;

            //    switch (httpEx.GetHttpCode())
            //    {
            //        case 404:
            //            action = "NotFound";
            //            break;

            //            // others if any
            //    }
            //}

            //if (ex is PermissionException)
            //    action = "NoPermission";

            //if(ex is DataNotFoundException)
            //    action = "NotFound";

            //httpContext.ClearError();
            //httpContext.Response.Clear();
            //httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            //httpContext.Response.TrySkipIisCustomErrors = true;

            //routeData.Values["controller"] = "Error";
            //routeData.Values["action"] = action;

            //controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            //((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));

            var httpContext = ((MvcApplication)sender).Context;
            var exp = Server.GetLastError();

            if (exp is DataNotFoundException)
            {
                httpContext.Response.Redirect("~/Error/NotFound");
            }
            if (exp is PermissionException)
            {
                httpContext.Response.Redirect("~/Error/NoPermission");
            }
            //else
            //{
            //    Response.Redirect(string.Format("~/Error/?error={0}", exp.Message));
            //}
        }
    }
}
