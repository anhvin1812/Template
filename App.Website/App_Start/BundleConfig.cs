using System.Web.Optimization;
using App.Website.Areas.Admin.App_Start;

namespace App.Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/coreJS").Include(
                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/moment.min.js", 
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/owl-carousel/js/owl.carousel.min.js",
                        "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js",
                        "~/Scripts/app/common.js",
                        "~/Scripts/layout.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/common").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/common.js"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Scripts/owl-carousel/css/owl.carousel.min.css",
                      "~/Scripts/owl-carousel/css/owl.theme.default.min.css",
                      "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css",
                      "~/Content/css/site.css",
                      "~/Content/css/responsive.css"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
