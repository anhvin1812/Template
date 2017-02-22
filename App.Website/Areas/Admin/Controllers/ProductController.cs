using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.Common;
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
        private IProductCategoryService ProductCategoryService { get; set; }

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
            : base(new IService[] { productService, productCategoryService })
        {
            ProductService = productService;
            ProductCategoryService = productCategoryService;
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
            var options = ProductCategoryService.GetOptionsForDropdownList(null, null);
            var status = ProductService.GetAllStatus().ToList();

            ViewBag.CategoryId = new SelectList(options.Items, options.DataValueField, options.DataTextField);
            ViewBag.StatusId = new SelectList(status, "Id", "Status");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(ProductEntry entry)
        {
            var options = ProductCategoryService.GetOptionsForDropdownList(null, null);
            var status = ProductService.GetAllStatus().ToList();

            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.CategoryId);
            ViewBag.Status = new SelectList(status, "Id", "Status", entry.StatusId);

            if (ModelState.IsValid)
            {
                ProductService.Insert(entry);
                return RedirectToAction("Index");
            }
            
            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var model = ProductService.GetProductForEditing(id);
            var options = ProductCategoryService.GetOptionsForDropdownList(null);
            var status = ProductService.GetAllStatus().ToList();
            var gallery = ProductService.GetGalleryByProductId(id);

            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField, model.CategoryId);
            ViewBag.Status = new SelectList(status, "Id", "Status", model.StatusId);
            ViewBag.Gallery = gallery;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, ProductUpdateEntry entry)
        {
            var options = ProductCategoryService.GetOptionsForDropdownList(null);
            var status = ProductService.GetAllStatus().ToList();
            var gallery = ProductService.GetGalleryByProductId(id);

            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.CategoryId);
            ViewBag.Status = new SelectList(status, "Id", "Status", entry.StatusId);
            ViewBag.Gallery = gallery;

            if (ModelState.IsValid)
            {
                ProductService.Update(id, entry);
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        #region Gallery
        [HttpPost]
        public ActionResult DeleteGallery(int productId, int galleryId)
        {
            ProductService.DeleteProductGallery(productId, galleryId);

            return Json(new SuccessResult());
        }
        #endregion

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
                    ProductCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}