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
        private ITagService TagService { get; set; }

        public NewsController(INewsService newsService, INewsCategoryService newsCategoryService, ITagService tagService)
            : base(new IService[] { newsService, newsCategoryService, tagService })
        {
            NewsService = newsService;
            NewsCategoryService = newsCategoryService;
            TagService = tagService;
        }

        #endregion


        public ActionResult Index(string keyword, int? categoryId = null, int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = NewsService.GetAll(keyword, categoryId, page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);

            ViewBag.Categories = new SelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField);
            ViewBag.Tags = new SelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(NewsEntry entry)
        {
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);

            ViewBag.Categories = new SelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, entry.CategoryIds);
            ViewBag.Tags = new SelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField, entry.TagIds);

            if (ModelState.IsValid)
            {
                NewsService.Insert(entry);
                return RedirectToAction("Index");
            }
            
            return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var model = NewsService.GetEntryForEditing(id);
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);

            ViewBag.Categories = new SelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, model.CategoryIds);
            ViewBag.Tags = new SelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField, model.TagIds);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, NewsUpdateEntry entry)
        {
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);

            ViewBag.Categories = new SelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, entry.CategoryIds);
            ViewBag.Tags = new SelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField, entry.TagIds);

            if (ModelState.IsValid)
            {
                NewsService.Update(id, entry);
                return RedirectToAction("Index");
            }

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
                    NewsService = null;
                    NewsCategoryService = null;
                    TagService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}