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
    public class ProductCategoryController : BaseController
    {

        #region Contractor
        private IProductCategoryService ProductCategoryService { get; set; }

        public ProductCategoryController(IProductCategoryService productCategoryService)
            : base(new IService[] { productCategoryService })
        {
            ProductCategoryService = productCategoryService;
        }

        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = ProductCategoryService.GetAll(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            int? recordCount = 0;
            var categories = ProductCategoryService.GetAll(null, null, ref recordCount);
            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(ProductCategoryEntry entry)
        {
            if (ModelState.IsValid)
            {
                ProductCategoryService.Insert(entry);
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            int? recordCount = 0;
            var categories = ProductCategoryService.GetAll(null, null, ref recordCount);
            ViewBag.ParentId = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, ProductCategoryEntry entry)
        {
            if (ModelState.IsValid)
            {
                ProductCategoryService.Insert(entry);
                return RedirectToAction("Index");
            }

            int? recordCount = 0;
            var categories = ProductCategoryService.GetAll(null, null, ref recordCount);
            ViewBag.ParentId = new SelectList(categories, "Id", "Name");

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
                    ProductCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}