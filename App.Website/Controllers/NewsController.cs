using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services;
using App.Services.NewsManagement;
using App.Services.ProductManagement;

namespace App.Website.Controllers
{
    public class NewsController : BaseController
    {
        #region Contractor
        private INewsService NewsService { get; set; }
        private INewsCategoryService NewsCategoryService { get; set; }

        public NewsController(INewsService newsService, INewsCategoryService newsCategoryService)
            :base(new IService[]{ newsService, newsCategoryService })
        {
            NewsService = newsService;
            NewsCategoryService = newsCategoryService;
        }
        #endregion


        // GET: News
        public ActionResult Index(int? page = null, int? pageSize = null)
        {

            return View();
        }


        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            var model = NewsService.GetById(id);

            int? recordCount = 0;
            ViewBag.Categories = NewsCategoryService.GetAll(null, null, null, ref recordCount);

            return View(model);
        }

        // GET: News/RelatedNews/5
        public ActionResult RelatedNews(int productId, int categoryId, int? maxRecords = null)
        {
            var result = NewsService.GetRelatedNews(productId, categoryId, maxRecords);

            return PartialView("_RelatedNews", result);
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
