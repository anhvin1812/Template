using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services.IdentityManagement;

namespace App.Website.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        #region Contractor
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = _roleService.GetAll(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
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
                    _roleService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}