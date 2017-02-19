﻿using System;
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

            TempData["CategoryId"] = new SelectList(options.Items, options.DataValueField, options.DataTextField);
            TempData["StatusId"] = new SelectList(status, "Id", "Status");

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

            var options = ProductCategoryService.GetOptionsForDropdownList(null, null);
            var status = ProductService.GetAllStatus().ToList();

            TempData["CategoryId"] = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.CategoryId);
            TempData["StatusId"] = new SelectList(status, "Id", "Status", entry.StatusId);

            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var options = ProductCategoryService.GetOptionsForDropdownList(null, null);
            var status = ProductService.GetAllStatus().ToList();

            //ViewBag.CategoryId = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.CategoryId);
            //ViewBag.StatusId = new SelectList(status, "Id", "Status", entry.StatusId);

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
                    ProductCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}