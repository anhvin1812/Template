using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.Common;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.Settings;
using App.Services.IdentityManagement;
using App.Services.NewsManagement;
using App.Services.Settings;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {

        #region Contractor
        private ISettingService SettingService { get; set; }
        private INewsCategoryService NewsCategoryService { get; set; }
        private IPublicNewsService PublicNewsService { get; set; }

        public SettingController(ISettingService settingService, INewsCategoryService newsCategoryService)
            : base(new IService[] { settingService, newsCategoryService })
        {
            SettingService = settingService;
            NewsCategoryService = newsCategoryService;
        }

        #endregion

        public ActionResult Homepage()
        {
            int? recordCount = null;

            var model = new SettingHomepageViewModel();
            model.HomepageLayOuts = SettingService.GetAllHomepageLayout();

            var addedCategoryIds = model.HomepageLayOuts.Where(x=>x.CategoryId != null).Select(x => x.CategoryId);
            model.Categories = NewsCategoryService.GetAll(false, null, null, ref recordCount).Where(x=> !addedCategoryIds.Contains(x.Id));

            return View(model);
        }

        [HttpPost]
        [ErrorHandler]
        public ActionResult Homepage(List<HomepageLayoutEntry> entries)
        {
            SettingService.UpdateHomepageLayout(entries);

            var layouts = SettingService.GetAllHomepageLayout();

            return Json(new SuccessResult(layouts, "Save successfully."), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Menu()
        {
            var model = SettingService.GetMenu();

            return View(model: model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Menu")]
        [ValidateInput(false)]
        public ActionResult Menu(string menu)
        {
            SettingService.UpdateMenu(menu);

            TempData["Message"] = "Saved successfully.";
            return RedirectToAction("Menu");
        }

        public ActionResult Options()
        {
            var options = SettingService.GetOptions();
            var model = new OptionEntry
            {
                Address = options.Address,
                Email = options.Email,
                Facebook = options.Facebook,
                PhoneNumber = options.PhoneNumber,
                Skype = options.Skype,
                Website = options.Website,
                LogoFileName = options.Logo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ErrorHandler(View = "Options")]
        public ActionResult Options(OptionEntry entry)
        {
            SettingService.UpdateOptions(entry);

            TempData["Message"] = "Saved successfully.";
            return RedirectToAction("Options");
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
                    SettingService = null;
                    NewsCategoryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}