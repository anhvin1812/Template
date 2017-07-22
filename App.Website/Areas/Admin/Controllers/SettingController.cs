using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.IdentityManagement;
using App.Services.IdentityManagement;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {

        #region Contractor
        private IRoleService RoleService { get; set; }

        public SettingController(IRoleService roleService)
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

        public ActionResult Homepage()
        {
            var model = RoleService.GetBlankRoleEntry();

            return View(model);
        }

        public ActionResult Menu()
        {
            var model = RoleService.GetBlankRoleEntry();

            return View(model);
        }

        [HttpPost]
        public ActionResult Menu(string menu)
        {
            var model = RoleService.GetBlankRoleEntry();

            return View(model);
        }

        public ActionResult Options()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(RoleEntry entry)
        {
            if (ModelState.IsValid)
            {
                RoleService.Insert(entry);
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var result = RoleService.GetRoleForEditing(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, RoleEntry entry)
        {
            RoleService.Update(id, entry);

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