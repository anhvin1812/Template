using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Permission;
using App.Services;
using App.Services.Dtos.Common;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.Settings;
using App.Services.IdentityManagement;
using App.Services.Settings;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    public class SettingController : BaseController
    {

        #region Contractor
        private ISettingService SettingService { get; set; }

        public SettingController(ISettingService settingService)
            : base(new IService[] { settingService })
        {
            SettingService = settingService;
        }

        #endregion

        public ActionResult Homepage()
        {
            var model = SettingService.GetAllHomepageLayout();

            return View(model);
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
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}