using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Index(int? page = 1, int? pageSize = 10, int? recordCount = null)
        {
            var result = UserService.GetAllUser(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserEntry entry)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, UserEntry entry)
        {
            return View();
        }

        #region Login

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

    #endregion

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