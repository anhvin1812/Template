using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.Common;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.IdentityManagement;
using App.Services.NewsManagement;
using App.Services.ProductManagement;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {

        #region Contractor
        private INewsService NewsService { get; set; }
        private INewsCategoryService NewsCategoryService { get; set; }

        public NewsController(INewsService newsService, INewsCategoryService newsCategoryService)
            : base(new IService[] { newsService, newsCategoryService })
        {
            NewsService = newsService;
            NewsCategoryService = newsCategoryService;
        }

        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = NewsService.GetAll(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            var options = NewsCategoryService.GetOptionsForDropdownList(null, null);

            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(NewsEntry entry)
        {
            var options = NewsCategoryService.GetOptionsForDropdownList(null, null);

            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.CategoryId);

            if (ModelState.IsValid)
            {
                NewsService.Insert(entry);
                return RedirectToAction("Index");
            }
            
            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var model = NewsService.GetProductForEditing(id);
            var options = NewsCategoryService.GetOptionsForDropdownList(null);

            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField, model.CategoryId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, NewsUpdateEntry entry)
        {
            var options = NewsCategoryService.GetOptionsForDropdownList(null);
            ViewBag.Categories = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.CategoryId);

            if (ModelState.IsValid)
            {
                NewsService.Update(id, entry);
                return RedirectToAction("Index");
            }

            return View("Index");
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
                    NewsService = null;
                    NewsCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}