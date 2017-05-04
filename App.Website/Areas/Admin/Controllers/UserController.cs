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
        private IRoleService RoleService { get; set; }

        public UserController(IUserService userService, IRoleService roleService)
            : base(new IService[] { userService })
        {
            UserService = userService;
            RoleService = roleService;
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
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(UserEntry entry)
        {
            var options = RoleService.GetOptionsForDropdownList();
            ViewBag.Roles = new SelectList(options.Items, options.DataValueField, options.DataTextField, options.SelectedValues);

            if (ModelState.IsValid)
            {
                UserService.Insert(entry);
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var options = RoleService.GetOptionsForDropdownList();
            ViewBag.Roles = new SelectList(options.Items, options.DataValueField, options.DataTextField, options.SelectedValues);

            var user = UserService.GetById(id);
            return View(DetailToEntry(user));
        }

        [HttpPost]
        public ActionResult Edit(int id, UserEntry entry)
        {
            var options = RoleService.GetOptionsForDropdownList();
            ViewBag.Roles = new SelectList(options.Items, options.DataValueField, options.DataTextField, options.SelectedValues);

            if (ModelState.IsValid)
            {
                UserService.Insert(entry);
                return RedirectToAction("Index");
            }

            return View(entry);
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

        #region Private Methods
        private UserEntry DetailToEntry(UserDetail detail)
        {
            return new UserEntry {
                Id = detail.Id,
                Firstname = detail.Firstname,
                Lastname = detail.Lastname,
                Email = detail.Email,
                EmailConfirmed = detail.EmailConfirmed,
                PhoneNumber = detail.PhoneNumber,
                PhoneNumberConfirmed = detail.PhoneNumberConfirmed,
                Address = detail.Address,
                LockoutEnabled = detail.LockoutEnabled,
                LockoutEndDateUtc = detail.LockoutEndDateUtc,
                Roles = detail.Roles.Select(x=>x.RoleId).ToList()
            };
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
                    RoleService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}