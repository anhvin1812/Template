using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.NewsManagement;
using App.Services.NewsManagement;
using App.Services.ProductManagement;
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
        public ActionResult Index(string keyword, int? page = 1)
        {
            int? recordCount = 0;
            var pageSize = 10;

            var result = PublicNewsService.GetAll(keyword, null, page, pageSize, ref recordCount);

            var pagedNews = new StaticPagedList<PublicNewsSummary>(result, page ?? 1, pageSize, (int)recordCount);

            ViewBag.Title = keyword;
            ViewBag.Filters = new NewsFilter
            {
                Keyword = keyword
            };

            return View(pagedNews);
        }

        public ActionResult Category(int id, int? page = 1)
        {
            var category = NewsCategoryService.GetById(id);
            if(category != null)
            {
                int? recordCount = 0;
                var pageSize = 10;

                var result = PublicNewsService.GetAll(null, id, page, pageSize, ref recordCount);

                var pagedNews = new StaticPagedList<PublicNewsSummary>(result, page ?? 1, pageSize, (int)recordCount);

                ViewBag.Title = category.Name;
                ViewBag.Filters = new NewsFilter
                {
                    CategoryId = id
                };

                return View(pagedNews);
            }

            return HttpNotFound();
        }

        public ActionResult Detail(int id)
        {
            var model = PublicNewsService.GetPublicNewsById(id);
            if (model == null)
                return HttpNotFound();

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
