using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;
using App.Core.Common;
using App.Website.Areas.Admin.Controllers;
using Microsoft.AspNet.Identity;

namespace App.Website.Fillters
{
    public class ServiceAuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Service Authorization Attribute Constructor
        /// </summary>
        //public ServiceAuthorizationAttribute() : this(null) { }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ServiceAuthorizationAttribute"/> class.
        ///// </summary>
        ///// <param name="httpContext">The HTTP context.</param>
        //public ServiceAuthorizationAttribute(IHttpContext httpContext)
        //{
        //    HttpContext = httpContext ?? new ServiceHttpContext(System.Web.HttpContext.Current);
        //    ClaimsRestrictionKey = null;
        //}

        /// <summary>
        /// Allows anonymous user
        /// </summary>
        public bool AllowAnonymous { get; set; }

        /// <summary>
        /// Configuration Key with a list if Restricted IPs and IP Ranges
        /// </summary>
        public string IpRangesRestrictionKey { get; set; }

        /// <summary>
        /// Configuration Key with a list of Claims to check
        /// </summary>
        public string ClaimsRestrictionKey { get; set; }

        /// <summary>
        /// Configuration Key with the Trusted Token
        /// </summary>
        public string TrustedTokenKey { get; set; }

        /// <summary>
        /// bool to check if cookie has valid token setup
        /// </summary>

        private bool IsCookieValid { get; set; }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
        //#if DEBUG
        //    var bypass = ConfigurationManager.AppSettings["BypassAuthorization"];

        //    if (bypass == "True")
        //    {
        //        base.OnAuthorization(authorizationContext);
        //        return;
        //    }

        //#endif

            if(AllowAnonymous != true && !filterContext.HttpContext.Request.IsAuthenticated)
            {
                var returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new {action = "Login", controller = "Account", Area = "Admin", returnUrl = returnUrl}));
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    claimsPrincipal = (ClaimsPrincipal)filterContext.HttpContext.User;
                    var currentController = filterContext.Controller as BaseController;

                    currentController?.SetIdentity(claimsPrincipal);
                }
                //base.OnAuthorization(filterContext);
            }
            
        }


        //private IHttpContext HttpContext { get; set; }
        private ClaimsPrincipal claimsPrincipal { get; set; }

    }
}