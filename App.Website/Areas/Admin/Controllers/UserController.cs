using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using App.Core.User;
using App.Entities.IdentityManagement;
using App.Services;
using App.Services.Dtos.IdentityManagement;
using App.Services.IdentityManagement;
using App.Website.Fillters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace App.Website.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {

        #region Contractor
        private Infrastructure.IdentityManagement.ApplicationSignInManager _signInManager;
        private Infrastructure.IdentityManagement.ApplicationUserManager _userManager;

        public Infrastructure.IdentityManagement.ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<Infrastructure.IdentityManagement.ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public Infrastructure.IdentityManagement.ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<Infrastructure.IdentityManagement.ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IUserService UserService { get; set; }
        private IRoleService RoleService { get; set; }

        public UserController(IUserService userService, IRoleService roleService)
            : base(new IService[] { userService, roleService })
        {
            UserService = userService;
            RoleService = roleService;
        }

        #endregion

        public ActionResult Index(string term, int? roleId, bool? lockoutEnabled, bool? emailConfirmed, int? page = 1, int? pageSize = 10)
        {
            int? recordCount = 0;
            var result = UserService.GetAll(term, lockoutEnabled, emailConfirmed, page, pageSize, ref recordCount);
            var pagedResult = new StaticPagedList<UserSummary>(result, page ?? 1, (int)pageSize, (int)recordCount);

            ViewBag.Filters = new UserFilter
            {
                Term = term,
                LockoutEnabled = lockoutEnabled,
                EmailConfirmed = emailConfirmed,
            };

            return View(pagedResult);
        }

        public ActionResult Create()
        {
            var options = RoleService.GetOptionsForDropdownList();
            var gender = UserService.GetGenderOptionsForDropdownList();
            ViewBag.Roles = new MultiSelectList(options.Items, options.DataValueField, options.DataTextField);
            ViewBag.Gender = new SelectList(gender.Items, gender.DataValueField, gender.DataTextField);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public async Task<ActionResult> Create(UserEntry entry)
        {
            var options = RoleService.GetOptionsForDropdownList();
            var gender = UserService.GetGenderOptionsForDropdownList();
            ViewBag.Roles = new MultiSelectList(options.Items, options.DataValueField, options.DataTextField, entry.RoleIds);
            ViewBag.Gender = new SelectList(gender.Items, gender.DataValueField, gender.DataTextField, entry.Gender);

            if (ModelState.IsValid)
            {
                UserService.Insert(entry);
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var user = UserService.GetById(id);

            var model = DetailToEntry(user);

            var options = RoleService.GetOptionsForDropdownList();
            var gender = UserService.GetGenderOptionsForDropdownList();
            ViewBag.Roles = new MultiSelectList(options.Items, options.DataValueField, options.DataTextField, model.RoleIds);
            ViewBag.Gender = new SelectList(gender.Items, gender.DataValueField, gender.DataTextField, model.Gender);

            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public async Task<ActionResult> Edit(int id, UserEntry entry)
        {
            var options = RoleService.GetOptionsForDropdownList();
            var gender = UserService.GetGenderOptionsForDropdownList();
            ViewBag.Roles = new MultiSelectList(options.Items, options.DataValueField, options.DataTextField, entry.RoleIds);
            ViewBag.Gender = new SelectList(gender.Items, gender.DataValueField, gender.DataTextField, entry.Gender);

            if (ModelState.IsValid)
            {
                UserService.Update(id, entry);
                TempData["Message"] = "Saved successfully.";
                return RedirectToAction("Edit", new { id = id });
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
                DateOfBirth = detail.DateOfBirth,
                Gender = (byte?)detail.Gender,
                Thumbnail = detail.ProfilePicture,
                LockoutEnabled = detail.LockoutEnabled,
                LockoutEndDateUtc = detail.LockoutEndDateUtc,
                RoleIds = detail.Roles.Select(x=>x.RoleId).ToList()
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