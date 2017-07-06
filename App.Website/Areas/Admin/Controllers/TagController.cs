using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.NewsManagement;
using App.Services.NewsManagement;
using App.Website.Fillters;

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


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = TagService.GetAll(page, pageSize, ref recordCount);

            return View(result);
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