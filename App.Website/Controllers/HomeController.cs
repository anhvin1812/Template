using System.Web.Mvc;
using App.Website.Fillters;

namespace App.Website.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize(AllowAnonymous = false)]
        public ActionResult Index()
        {
            return View();
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
    }
}