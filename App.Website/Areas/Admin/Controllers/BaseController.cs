using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Services;

namespace App.Website.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(IEnumerable<IService> services)
        {
            Services = services;
        }

        protected IEnumerable<IService> Services { get; private set; }
    }
}