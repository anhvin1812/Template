using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using App.Services;
using App.Services.IdentityManagement;

namespace App.Website.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        #region Contractor
        private IUserService UserService { get; set; }

        public UserController(IUserService userService)
            : base(new IService[] { userService })
        {
            UserService = userService;
        }

        #endregion

        public ActionResult Index([FromUri(Name = "p")] int? page = null, [FromUri(Name = "ps")] int? pageSize = null, [FromUri(Name = "rc")] int? recordCount = null)
        {
            var result = UserService.GetAllUser(page, pageSize, ref recordCount);

            return View(result);
        }


        #region Dispose

        private bool _disposed = false;
        
        [System.Web.Mvc.NonAction]
        protected override void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    UserService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}