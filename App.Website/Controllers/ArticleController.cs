using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.NewsManagement;
using App.Services.NewsManagement;
using App.Services.ProductManagement;
using App.Website.Fillters;
using PagedList;

namespace App.Website.Controllers
{
    public class ArticleController : BaseController
    {
        #region Contractor
        private IPublicNewsService PublicNewsService { get; set; }
        private INewsCategoryService NewsCategoryService { get; set; }

        public ArticleController(IPublicNewsService publicNewsService, INewsCategoryService newsCategoryService)
            :base(new IService[]{ publicNewsService })
        {
            PublicNewsService = publicNewsService;
            NewsCategoryService = newsCategoryService;
        }
        #endregion


        // GET: News
        [LayoutActionFilter]
        public ActionResult Index(string keyword, int? page = 1)
        {
            int? recordCount = 0;
            var pageSize = 10;

            var result = PublicNewsService.GetAll(keyword, null, page, pageSize, ref recordCount);

            dynamic model = new ExpandoObject();
            model.PagedNews = new StaticPagedList<PublicNewsSummary>(result, page ?? 1, pageSize, (int)recordCount);
            model.NewsFilter = new NewsFilter
            {
                Keyword = keyword
            };

            return View(model);
        }

        [LayoutActionFilter]
        public ActionResult Category(int id, int? page = 1)
        {
            var category = NewsCategoryService.GetById(id);
            if(category != null)
            {
                int? recordCount = 0;
                var pageSize = 10;

                var result = PublicNewsService.GetAll(null, id, page, pageSize, ref recordCount);

                dynamic model = new ExpandoObject();
                model.PagedNews = new StaticPagedList<PublicNewsSummary>(result, page ?? 1, pageSize, (int)recordCount);
                model.Category = new PublicCategorySummary
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return View(model);
            }

            return HttpNotFound();
        }

        [LayoutActionFilter]
        public ActionResult Detail(int id)
        {
            var model = PublicNewsService.GetPublicNewsById(id);
            if (model == null)
                return HttpNotFound();

            ViewBag.SocialMetaTags = model;

            return View(model);
        }


        public ActionResult Featured()
        {
            var model = PublicNewsService.GetFeaturedNews();

            return PartialView("_Featured", model);
        }

        public ActionResult Hot()
        {
            var model = PublicNewsService.GetHotNews();

            return PartialView("_Hot", model);
        }

        public ActionResult Related(int newsId, int categoryId)
        {
            var model = PublicNewsService.GetRelatedNews(newsId, categoryId);

            return PartialView("_Related", model);
        }

        public ActionResult Latest(int categoryId)
        {
            var model = PublicNewsService.GetLatestNews(categoryId);

            return PartialView("_Latest", model);
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
                    PublicNewsService = null;
                    NewsCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}
