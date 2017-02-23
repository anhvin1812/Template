using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.NewsManagement;
using App.Services.NewsManagement;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class NewsCategoryController : BaseController
    {

        #region Contractor
        private INewsCategoryService NewsCategoryService { get; set; }

        public NewsCategoryController(INewsCategoryService newsCategoryService)
            : base(new IService[] { newsCategoryService })
        {
            NewsCategoryService = newsCategoryService;
        }

        #endregion


        public ActionResult Index(int? page = null, int? pageSize = null)
        {
            int? recordCount = 0;
            var result = NewsCategoryService.GetAll(page, pageSize, ref recordCount);

            return View(result);
        }

        public ActionResult Create()
        {
            var options = NewsCategoryService.GetOptionsForDropdownList(null, null);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField);
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Create")]
        public ActionResult Create(NewsCategoryEntry entry)
        {
            var options = NewsCategoryService.GetOptionsForDropdownList(null, null);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.ParentId);

            if (ModelState.IsValid)
            {
                NewsCategoryService.Insert(entry);
                return RedirectToAction("Index");
            }

           return View(entry);
        }

        public ActionResult Edit(int id)
        {
            var model = NewsCategoryService.GetCategoryForEditing(id);

            var options = NewsCategoryService.GetOptionsForDropdownList(null, id);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField, model.ParentId, options.DisabledValues);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Edit")]
        public ActionResult Edit(int id, NewsCategoryEntry entry)
        {
            var options = NewsCategoryService.GetOptionsForDropdownList(null, id);
            ViewBag.Parents = new SelectList(options.Items, options.DataValueField, options.DataTextField, entry.ParentId, options.DisabledValues);

            if (ModelState.IsValid)
            {
                NewsCategoryService.Update(id, entry);
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
                    NewsCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}