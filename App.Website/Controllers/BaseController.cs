﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using App.Services;

namespace App.Website.Controllers
{
    public abstract class BaseController : Controller
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