using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.IdentityManagement;
using App.Services.IdentityManagement;
using App.Website.Fillters;
using Microsoft.AspNet.Identity.Owin;

namespace App.Website.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        #region Contractor
        private IUserService UserService { get; set; }

        public UserController(IUserService userService)
            : base(new IService[] { userService })
        {
            UserService = userService;
        }

        #endregion

        public ActionResult Index([FromUri(Name = "p")] int? page = null, [FromUri(Name = "ps")] int? pageSize = null, [FromUri(Name = "rc")] int? recordCount = null)
        {
            var result = UserService.GetAllUser(page, pageSize, ref recordCount);

            return View(result);
        }
        [System.Web.Mvc.OverrideAuthorization]
        [ServiceAuthorization(AllowAnonymous = true)]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [System.Web.Mvc.OverrideAuthorization]
        [ServiceAuthorization(AllowAnonymous = true)]
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var signIn = HttpContext.GetOwinContext().Get<Infrastructure.IdentityManagement.ApplicationSignInManager>();
            var result = signIn.PasswordSignIn(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #region Dispose

        private bool _disposed = false;
        
        [System.Web.Mvc.NonAction]
        protected override void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    UserService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}