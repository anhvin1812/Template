using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.IdentityManagement;
using App.Services.ProductManagement;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        #region Contractor
        private IProductService ProductService { get; set; }

        public ProductController(IProductService productService)
            : base(new IService[] { productService })
        {
            ProductService = productService;
        }

        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = ProductService.GetAll(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            //var model = ProductService.In();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(ProductEntry entry)
        {
            if (ModelState.IsValid)
            {
                ProductService.Insert(entry);
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
           // var result = RoleService.GetRoleForEditing(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, RoleEntry entry)
        {
            //RoleService.Update(id, entry);

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
                    ProductService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}