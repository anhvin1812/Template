using System.Web.Mvc;
using App.Services;
using App.Services.Dtos.Common;
using App.Website.Fillters;

namespace App.Website.Areas.Admin.Controllers
{
    [ServiceAuthorization(Roles = "Administrator")]
    public class ElmahController : BaseController
    {
        public ElmahController()
            : base(new IService[] {  })
        {
        }

        public ActionResult Index(string type)
        {
            return new ElmahResult(type);
        }
      
    }
    
}