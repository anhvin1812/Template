using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.Security;

namespace App.Website.Fillters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
      
        //private readonly string[] allowedroles;
        //public CustomAuthorizeAttribute(params string[] roles)
        //{
        //    this.allowedroles = roles;
        //}

        public bool AllowAnonymous { get; set; }

        //public CustomAuthorizeAttribute(bool allowAnonymous)
        //{
        //    this.AllowAnonymous = allowAnonymous;
        //}

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{

        //    var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

        //    if (!AllowAnonymous)
        //    {
        //        if (!principal.Identity.IsAuthenticated)
        //        {
        //            return ;
        //        }

        //        var userName = principal.FindFirst(ClaimTypes.Name).Value;
        //        var userAllowedTime = principal.FindFirst("userAllowedTime").Value;

        //        if (userName != "Admin")
        //        {
        //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Not allowed to access...bla bla");
        //            return ;
        //        }
        //    }
            

        //    //User is Authorized, complete execution
        //    return ;

        //}

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AllowAnonymous)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
                }

                //var userName = filterContext.HttpContext.User.Identity.Name;
                ////var userAllowedTime = principal.FindFirst("userAllowedTime").Value;

                //if (userName == "Admin")
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admin", controller = "Home", action = "Index"}));
                    
                //}
            }


            //User is Authorized, complete execution
            return;
        }
    }  
}