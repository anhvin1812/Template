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
using PagedList;

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


        public ActionResult Index(string keyword, int? categoryId = null, int? statusId = null, int? mediaTypeId = null, bool? hot = null, bool? featured = null, int? page = 1, int? pageSize = 15)
        {
            int? recordCount = 0;
            var result = NewsService.GetAll(keyword, categoryId, statusId, mediaTypeId, hot, featured, page, pageSize, ref recordCount);

            var pagedNews = new StaticPagedList<NewsSummary>(result, page ?? 1, (int) pageSize, (int)recordCount);

            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var statusOptions = NewsService.GetStatusOptionsForDropdownList();
            var mediaTypeOptions = NewsService.GetMediaTypeOptionsForDropdownList();

            ViewBag.Categories = new SelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, categoryId);
            ViewBag.Status = new SelectList(statusOptions.Items, statusOptions.DataValueField, statusOptions.DataTextField, statusId);
            ViewBag.MediaTypes = new SelectList(mediaTypeOptions.Items, mediaTypeOptions.DataValueField, mediaTypeOptions.DataTextField, mediaTypeId);
            ViewBag.Filters = new NewsFilter
            {
                Keyword = keyword,
                CategoryId = categoryId,
                StatusId = statusId,
                MediaTypeId = mediaTypeId,
                IsHot = hot,
                IsFeatured = featured
            };

            return View(pagedNews);
        }

        public ActionResult Create()
        {
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);
            var statusOptions = NewsService.GetStatusOptionsForDropdownList();
            var mediaTypeOptions = NewsService.GetMediaTypeOptionsForDropdownList();

            ViewBag.Categories = new MultiSelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField);
            ViewBag.Tags = new MultiSelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField);
            ViewBag.Status = new SelectList(statusOptions.Items, statusOptions.DataValueField, statusOptions.DataTextField);
            ViewBag.MediaTypes = new SelectList(mediaTypeOptions.Items, mediaTypeOptions.DataValueField, mediaTypeOptions.DataTextField);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(NewsEntry entry)
        {
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);
            var statusOptions = NewsService.GetStatusOptionsForDropdownList();
            var mediaTypeOptions = NewsService.GetMediaTypeOptionsForDropdownList();

            ViewBag.Categories = new MultiSelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, entry.CategoryIds);
            ViewBag.Tags = new MultiSelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField, entry.TagIds);
            ViewBag.Status = new SelectList(statusOptions.Items, statusOptions.DataValueField, statusOptions.DataTextField, entry.StatusId);
            ViewBag.MediaTypes = new SelectList(mediaTypeOptions.Items, mediaTypeOptions.DataValueField, mediaTypeOptions.DataTextField, entry.MediaTypeId);

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
            var statusOptions = NewsService.GetStatusOptionsForDropdownList();
            var mediaTypeOptions = NewsService.GetMediaTypeOptionsForDropdownList();

            ViewBag.Categories = new MultiSelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, model.CategoryIds);
            ViewBag.Tags = new MultiSelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField, model.TagIds);
            ViewBag.Status = new SelectList(statusOptions.Items, statusOptions.DataValueField, statusOptions.DataTextField, model.StatusId);
            ViewBag.MediaTypes = new SelectList(mediaTypeOptions.Items, mediaTypeOptions.DataValueField, mediaTypeOptions.DataTextField, model.MediaTypeId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, NewsUpdateEntry entry)
        {
            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var tagOptions = TagService.GetOptionsForDropdownList(false);
            var statusOptions = NewsService.GetStatusOptionsForDropdownList();
            var mediaTypeOptions = NewsService.GetMediaTypeOptionsForDropdownList();

            ViewBag.Categories = new MultiSelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField, entry.CategoryIds);
            ViewBag.Tags = new MultiSelectList(tagOptions.Items, tagOptions.DataValueField, tagOptions.DataTextField, entry.TagIds);
            ViewBag.Status = new SelectList(statusOptions.Items, statusOptions.DataValueField, statusOptions.DataTextField, entry.StatusId);
            ViewBag.MediaTypes = new SelectList(mediaTypeOptions.Items, mediaTypeOptions.DataValueField, mediaTypeOptions.DataTextField, entry.MediaTypeId);

            if (ModelState.IsValid)
            {
                NewsService.Update(id, entry);
                TempData["Message"] = "Saved successfully.";
                return RedirectToAction("Edit", new {id = id});
            }

            return View(entry);
        }

        public ActionResult Preview(NewsUpdateEntry entry)
        {
            if (ModelState.IsValid)
            {
                var model = NewsService.Preview(entry);
                return View("~/Views/Article/Detail.cshtml", model: model, masterName: "~/Views/Shared/_Layout.cshtml");
            }

            return Content("");
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