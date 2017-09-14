using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Core.Email
{
    public abstract class Mail
    {
        
        public List<string> EmailsTo { get; protected set; }
        public string Subject { get; protected set; }
        public string Body { get; protected set; }

        protected Mail(string subject, string emailTo)
        {
            this.Subject = subject;
            this.EmailsTo = new List<string>() {emailTo};
        }

        protected string GenerateBodyFromViewRazor(object model, string filePath)
        {
            var st = new StringWriter();
            var context = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            var controllerContext = new ControllerContext(new RequestContext(context, routeData), new FakeController());
            var razor = new RazorView(controllerContext, filePath, null, false, null);
            razor.Render(new ViewContext(controllerContext, razor, new ViewDataDictionary(model), new TempDataDictionary(), st), st);
            return st.ToString();
        }

        private class FakeController : Controller
        {

        }
    }
}
