using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services;
using App.Services.ProductManagement;

namespace App.Website.Controllers
{
    public class ProductController : BaseController
    {
        #region Contractor
        private IProductService ProductService { get; set; }
        private IProductCategoryService ProductCategoryService { get; set; }

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
            :base(new IService[]{productService})
        {
            ProductService = productService;
            ProductCategoryService = productCategoryService;
        }
        #endregion


        // GET: Product
        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = ProductService.GetAll(page, pageSize, ref recordCount);

            ViewBag.Categories = ProductCategoryService.GetAll(null, null, ref recordCount);

            return View(result);
        }

        // GET: Product/Category/5
        public ActionResult Category(int id)
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var model = ProductService.GetById(id);
            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
