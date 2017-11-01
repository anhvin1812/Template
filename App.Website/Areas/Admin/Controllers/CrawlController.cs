
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
using App.Services.IdentityManagement;
using App.Services.NewsManagement;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class CrawlController : BaseController
    {

        #region Contractor
        private ICrawlService CrawlService { get; set; }
        private INewsCategoryService NewsCategoryService { get; set; }
        private INewsService NewsService { get; set; }

        public CrawlController(ICrawlService crawlService, INewsService newsService, INewsCategoryService newsCategoryService)
            : base(new IService[] { crawlService, newsService, newsCategoryService })
        {
            CrawlService = crawlService;
            NewsService = newsService;
            NewsCategoryService = newsCategoryService; 
        }

        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            var crawlSourcesOptions = CrawlService.GetCrawlSourceOptionsForDropdownList();
            ViewBag.CrawlSources = new SelectList(crawlSourcesOptions.Items, crawlSourcesOptions.DataValueField, crawlSourcesOptions.DataTextField);

            return View();
        }

        [HttpPost]
        [ErrorHandler]
        public ActionResult Scan(CrawlFilter filter)
        {
            var results = CrawlService.Scan(filter);
            return PartialView("_ArticleItem", results);
        }

        [HttpPost]
        [ErrorHandler]
        public ActionResult Save(CrawlEntry entry)
        {
            CrawlService.Save(entry);

            return Json(new SuccessResult("Saved Successfully!"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ErrorHandler]
        public ActionResult GetArticleDetail(int sourceId, string linkDetail)
        {
            var result = CrawlService.GetArticleDetail(sourceId, linkDetail);
            var model = new CrawlEntry
            {
                Title = result.Title,
                Description = result.Description,
                Content = result.Content,
                Date = result.Date,
                Categories = result.Categories,
                Tags = result.Tags
            };

            var categoryOptions = NewsCategoryService.GetOptionsForDropdownList(null, null, false);
            var statusOptions = NewsService.GetStatusOptionsForDropdownList();
            var mediaTypeOptions = NewsService.GetMediaTypeOptionsForDropdownList();

            ViewBag.Categories = new MultiSelectList(categoryOptions.Items, categoryOptions.DataValueField, categoryOptions.DataTextField);
            ViewBag.Status = new SelectList(statusOptions.Items, statusOptions.DataValueField, statusOptions.DataTextField);
            ViewBag.MediaTypes = new SelectList(mediaTypeOptions.Items, mediaTypeOptions.DataValueField, mediaTypeOptions.DataTextField);

            return PartialView("_SaveForm", model);
        }

        #region Private Methods

        #endregion

        #region Json result
        public JsonResult Pages(int sourceId)
        {
            var result = CrawlService.GetPagesBySourceId((sourceId)).Select(x => new
            {
                id = x.Id,
                text = x.Name
            });

            return Json(new SuccessResult(result), JsonRequestBehavior.AllowGet);
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
                    CrawlService = null;
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