using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using App.Services;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    [ServiceAuthorization(AllowAnonymous = false)]
    public class BaseController : Controller
    {
        protected BaseController(IEnumerable<IService> services)
        {
            Services = services;
        }

        protected IEnumerable<IService> Services { get; private set; }

        protected internal ClaimsPrincipal CurrentClaimsIdentity { get; private set; }

        internal void SetIdentity(ClaimsPrincipal identity)
        {
            CurrentClaimsIdentity = identity;
            foreach (var service in Services)
            {
                service.SetIdentity(identity);
            }

        }

        //protected override void OnAuthentication(AuthenticationContext filterContext)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        //var returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
        //        filterContext.Result = RedirectToAction("Login", "User", new { Area = "Admin", returnUrl = "" });

        //    }
        //}
    }
}