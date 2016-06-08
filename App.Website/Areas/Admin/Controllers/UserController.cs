using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace App.Website.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Index([FromUri(Name = "p")] int? page = null, [FromUri(Name = "ps")] int? pageSize = null, [FromUri(Name = "rc")] int? recordCount = null)
        {
            return View();
        }
    }
}