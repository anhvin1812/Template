using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Website.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Index(int? page = null, int? pageSize = null, int? recordCount = null)
        {
            return View();
        }
    }
}