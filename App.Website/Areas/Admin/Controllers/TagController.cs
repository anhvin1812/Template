using System.Linq;
using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.Common;
using App.Services.Dtos.NewsManagement;
using App.Services.NewsManagement;
using App.Website.Fillters;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace App.Website.Areas.Admin.Controllers
{
    public class TagController : BaseController
    {

        #region Contractor
        private ITagService TagService{ get; set; }

        public TagController(ITagService tagService)
            : base(new IService[] { tagService })
        {
            TagService = tagService;
        }

        #endregion


        public ActionResult Index(string keyword, bool? isDisabled = null, int? page = 1, int? pageSize = 15)
        {
            int? recordCount = 0;
            var result = TagService.GetAll(keyword, isDisabled, page, pageSize, ref recordCount);
            var pagedTags = new StaticPagedList<TagSummary>(result, (int)page, (int)pageSize, (int)recordCount);

            ViewBag.Keyword = keyword;
            return View(pagedTags);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(TagEntry entry)
        {
            if (ModelState.IsValid)
            {
                TagService.Insert(entry);
                return RedirectToAction("Index");
            }

           return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var model = TagService.GetEntryForEditing(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, TagEntry entry)
        {
            if (ModelState.IsValid)
            {
                TagService.Update(id, entry);
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        #region Json result
        public ActionResult TagsIn(string query,  string ids)
        {
            if (!ids.IsNullOrWhiteSpace())
            {
                var results = TagService.GetByStringIds(ids, false).Select(x => new
                {
                    id = x.Id,
                    text = x.Name
                });

                return Json(new SuccessResult(results), JsonRequestBehavior.AllowGet);
            }

            int? recordCount = 0;
            var result = TagService.GetAll(query, false, 1, 10, ref recordCount).Select( x=> new
                {
                    id= x.Id,
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
                    TagService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}