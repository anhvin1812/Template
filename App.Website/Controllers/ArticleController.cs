using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Extensions;
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
        private INewsService NewsService { get; set; }
        private INewsCategoryService NewsCategoryService { get; set; }

        public ArticleController(IPublicNewsService publicNewsService, INewsService newsService, INewsCategoryService newsCategoryService)
            :base(new IService[]{ publicNewsService })
        {
            PublicNewsService = publicNewsService;
            NewsService = newsService;
            NewsCategoryService = newsCategoryService;
        }
        #endregion


        [LayoutActionFilter]
        public ActionResult Index(string keyword, DateTime? startDate = null, DateTime? endDate = null, int? page = 1)
        {
            int? recordCount = 0;
            var pageSize = 10;

            var result = PublicNewsService.GetAll(keyword, startDate, endDate, null, page, pageSize, ref recordCount);

            var model = new ArticleSearch();
            model.PagedNews = new StaticPagedList<PublicNewsSummary>(result, page ?? 1, pageSize, (int)recordCount);
            model.NewsFilter = new NewsFilter
            {
                Keyword = keyword,
                StartDate = startDate,
                EndDate = endDate
            };

            return View(model);
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

        [LayoutActionFilter]
        public ActionResult Preview(NewsUpdateEntry entry)
        {
            if (ModelState.IsValid)
            {
                var model = NewsService.Preview(entry);
                return View(model);
            }

            return Content("");
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
            var category = NewsCategoryService.GetById(categoryId);
            if(category!=null)
            {
                var news = PublicNewsService.GetLatestNews(categoryId);
                var model = new LatestNews {
                    Category = new PublicCategorySummary{
                        Id = category.Id,
                        Name = category.Name
                    },
                    News = news
                };

                return PartialView("_Latest", model);
            }

            return Content("");
        }

        public ActionResult TopViewSideBar()
        {
            var now = DateTime.Now.Date;

            var startDateOfWeek = DateTimeExtension.GetMonday(now);
            var endDateOfWeek = startDateOfWeek.AddDays(6);

            var startDateOfMonth = new DateTime(now.Year, now.Month, 1);
            var endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1);

            var model = new TopViewSidebar
            {
                Weekly = PublicNewsService.GetMostViews(startDateOfWeek, endDateOfWeek),
                Monthly = PublicNewsService.GetMostViews(startDateOfMonth, endDateOfMonth),
                All = PublicNewsService.GetMostViews(),
            }; 

            return PartialView("_TopViewSideBar", model);
        }

        public ActionResult BreadCrumb(int categoryId)
        {
            var categories = NewsCategoryService.GetCategoryWithParents(categoryId);

            if (categories != null && categories.Any())
            {
                var model = new Breadcrumb
                {
                    Title = categories.LastOrDefault()?.Name,
                    Categories = categories
                };
                  
                return PartialView("_Breadcrumb", model);
            }

            return Content("");
        }

        #region Category
        [LayoutActionFilter]
        public ActionResult Category(int id, int? page = 1)
        {
            var categories = NewsCategoryService.GetCategoryWithParents(id);

            if (categories != null && categories.Any())
            {
                int? recordCount = 0;
                var pageSize = 10;

                var results = PublicNewsService.GetAll(null,null, null, id, page, pageSize, ref recordCount);

                var model = new ArticleCategory();
                model.PagedNews = new StaticPagedList<PublicNewsSummary>(results, page ?? 1, pageSize, (int)recordCount);
                model.Categories = categories;

                return View(model);
            }

            return HttpNotFound();
        }

        public ActionResult CategoriesSideBar(int? categoryId)
        {
            var categories = NewsCategoryService.GetByParentId(categoryId, false, true);

            if (categories != null && categories.Any())
            {
                return PartialView("_CategoriesSideBar", categories);
            }

            return Content("");
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
                    PublicNewsService = null;
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
