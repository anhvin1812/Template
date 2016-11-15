using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.IdentityManagement;
using App.Services.IdentityManagement;

namespace App.Website.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {

        #region Contractor
        private IRoleService RoleService { get; set; }

        public RoleController(IRoleService roleService)
            : base(new IService[] { roleService })
        {
            RoleService = roleService;
        }

        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = RoleService.GetAll(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            var model = new RoleEntry
            {
                Permissions = new List<PermissionEntry>
                {
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Create},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Read},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Modify},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Delete},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Super},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Create},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Read},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Modify},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Delete},
                    new PermissionEntry {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Super},
                }
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RoleEntry entry)
        {
            RoleService.Insert(entry);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var result = RoleService.GetById(id);
            return View(result);
        }

        [HttpPost]
        [Route("{id:int}")]
        public ActionResult Edit(int id, RoleEntry entry)
        {
            RoleService.Insert(entry);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        #region Dispose
        private bool _disposed;

        [NonAction]
        protected override void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    RoleService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}