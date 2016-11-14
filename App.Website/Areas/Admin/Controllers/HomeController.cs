using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services;
using App.Services.IdentityManagement;

namespace App.Website.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {

        #region Contractor
        private IUserService UserService { get; set; }

        public HomeController(IUserService userService)
            : base(new IService[] { userService })
        {
            UserService = userService;
        }

        #endregion
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}