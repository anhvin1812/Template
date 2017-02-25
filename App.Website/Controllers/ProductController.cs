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
            var result = ProductService.GetAll(null,null, page, pageSize, ref recordCount);

            ViewBag.Categories = ProductCategoryService.GetAll(null, null, ref recordCount);

            return View(result);
        }

        // GET: Product/NewProducts/5
        public ActionResult NewProducts(int? maxRecords = null)
        {
            int? recordCount = 0;
            var result = ProductService.GetAll(null, null, 1, maxRecords, ref recordCount);

            return View("_ProductList", result);
        }

        // GET: Product/Category/5
        public ActionResult Category(int id)
        {
            int? recordCount = 0;
            var result = ProductService.GetAll(null, id,null, null, ref recordCount);

            ViewBag.Categories = ProductCategoryService.GetAll(null, null, ref recordCount);
            ViewBag.CategoryId = id;

            return View("Index", result);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var model = ProductService.GetById(id);
            return View(model);
        }

        // GET: Product/RelatedProducts/5
        public ActionResult RelatedProducts(int productId, int categoryId, int? maxRecords = null)
        {
            var result = ProductService.GetRelatedProducts(productId, categoryId, maxRecords);

            return PartialView("_RelatedProducts", result);
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
