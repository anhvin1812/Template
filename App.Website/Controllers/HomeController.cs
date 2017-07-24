using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.Settings;
using App.Services.Settings;
using App.Website.Fillters;


namespace App.Website.Controllers
{
    public class HomeController : BaseController
    {
        #region Contractor
        private ISettingService SettingService { get; set; }

        public HomeController(ISettingService settingService)
            : base(new IService[] { settingService })
        {
            SettingService = settingService;
        }

        #endregion
        [LayoutActionFilter]
        public ActionResult Index()
        {
            var model = new HomepageViewModel();
            model.Layouts = SettingService.GetAllHomepageLayout();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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