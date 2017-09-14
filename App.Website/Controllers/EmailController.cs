using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.Settings;
using App.Services.Settings;
using App.Website.Fillters;


namespace App.Website.Controllers
{
    public class EmailController : BaseController
    {
        #region Contractor
        //private ISettingService SettingService { get; set; }

        public EmailController(/*ISettingService settingService*/)
            : base(new IService[] {})
        {
            //SettingService = settingService;
        }

        #endregion
        public ActionResult Index(string templateName)
        {
            return View($"~/EmailTemplates/{templateName}.cshtml");
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
                    //SettingService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}