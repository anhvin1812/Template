using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Website.ModelBinder
{
    public class AppModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);
            // Assign the Mode to TempData
            controllerContext.Controller.TempData["model"] = bindingContext.Model;
        }
    }
}