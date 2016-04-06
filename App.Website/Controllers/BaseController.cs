using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using App.Services;

namespace App.Website.Controllers
{
   public abstract class BaseController : Controller
    {
        protected MembershipUser LoggedOnReadOnlyUser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggingService"> </param>
        /// <param name="unitOfWorkManager"> </param>
        /// <param name="membershipService"></param>
        /// <param name="localizationService"> </param>
        /// <param name="roleService"> </param>
        /// <param name="settingsService"> </param>
        public BaseController(IEnumerable<IService> services)
        {
            Services = services;
        }

       /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        protected IEnumerable<IService> Services { get; private set; }

        /// <summary>
        /// To be injected using property injection.
        /// </summary>
        [Obsolete("This is a badly design feature, please do not use or copy.")]
        public ISecurityService BaseSecurityService { get; set; }


       /// <summary>
        /// Gets the current claims identity.
        /// </summary>
        /// <value>
        /// The current claims identity.
        /// </value>
        internal protected ClaimsPrincipal CurrentClaimsIdentity { get; private set; }

       internal void SetIdentity(ClaimsPrincipal identity)
        {
            CurrentClaimsIdentity = identity;
            foreach (var service in Services)
            {
                service.SetIdentity(identity);
            }

            if (BaseSecurityService != null) BaseSecurityService.SetIdentity(identity);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RouteData.Values["controller"];
            var action = filterContext.RouteData.Values["action"];
            var area = filterContext.RouteData.DataTokens["area"] ?? string.Empty;
            var settings = SettingsService.GetSettings();

            //// Check if forum is closed
            //if (settings.IsClosed && !filterContext.IsChildAction)
            //{
            //    // Only redirect if its closed and user is NOT in the admin
            //    if (controller.ToString().ToLower() != "closed" && controller.ToString().ToLower() != "members" && !area.ToString().ToLower().Contains("admin"))
            //    {
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Closed" }, { "action", "Index" } });
            //    }          
            //}

            // Check if they need to agree to permissions
            if (SettingsService.GetSettings().AgreeToTermsAndConditions == true && !filterContext.IsChildAction && LoggedOnReadOnlyUser != null && LoggedOnReadOnlyUser.HasAgreedToTermsAndConditions != true)
            {
                // Only redirect if its closed and user is NOT in the admin
                if (action.ToString().ToLower() != "termsandconditions" && !area.ToString().ToLower().Contains("admin"))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "TermsAndConditions" } });
                }
            }

            // If the forum is new members need approving and the user is not approved, log them out
            if (LoggedOnReadOnlyUser != null && !LoggedOnReadOnlyUser.IsApproved && settings.NewMemberEmailConfirmation == true)
            {
                FormsAuthentication.SignOut();
                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                {
                    Message = LocalizationService.GetResourceString("Members.MemberEmailAuthorisationNeeded"),
                    MessageType = GenericMessages.success
                };
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }

            // If the user is banned - Log them out.
            if (LoggedOnReadOnlyUser != null && LoggedOnReadOnlyUser.IsBanned)
            {
                FormsAuthentication.SignOut();
                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                {
                    Message = LocalizationService.GetResourceString("Members.NowBanned"),
                    MessageType = GenericMessages.danger
                };
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }
        }

       protected bool UserIsAuthenticated
       {
           get
           {
               return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
           }
       }

       protected bool UserIsAdmin
       {
           get
           {
               return User.IsInRole("Admin");
           }
       }

        protected void ShowMessage(GenericMessageViewModel messageViewModel)
        {
            //ViewData[AppConstants.MessageViewBagName] = messageViewModel;
            TempData[AppConstants.MessageViewBagName] = messageViewModel;
        }

       protected string Username
       {
           get
           {
               return UserIsAuthenticated ? System.Web.HttpContext.Current.User.Identity.Name : null;
           }
       } 

        internal ActionResult ErrorToHomePage(string errorMessage)
        {
            // Use temp data as its a redirect
            TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
            {
                Message = errorMessage,
                MessageType = GenericMessages.danger
            };
            // Not allowed in here so
            return RedirectToAction("Index", "Home");
        }
    }
}