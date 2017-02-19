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
            var options = ProductCategoryService.GetOptionsForDropdownList(null, null);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField);
            
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

            var options = ProductCategoryService.GetOptionsForDropdownList(null, null);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.ParentId);

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var model = ProductCategoryService.GetCategoryForEditing(id);

            var options = ProductCategoryService.GetOptionsForDropdownList(null, id);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField, model.ParentId, options.DisabledValues);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, ProductCategoryEntry entry)
        {
            if (ModelState.IsValid)
            {
                ProductCategoryService.Update(id, entry);
                return RedirectToAction("Index");
            }

            var options = ProductCategoryService.GetOptionsForDropdownList(null, id);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.ParentId, options.DisabledValues);

            return View(entry);
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