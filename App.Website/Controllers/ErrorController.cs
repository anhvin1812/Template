using System;
using System.Web.Mvc;
using App.Website.Fillters;

namespace App.Website.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult NotFound()
        {
            return View("NotFound");
        }
    }
}