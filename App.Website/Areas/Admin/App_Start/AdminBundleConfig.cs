using System.Web.Optimization;

namespace App.Website.Areas.Admin.App_Start
{
    internal class AdminBundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                        getPath("Scripts/jQuery/jQuery-2.1.4.min.js"),
                        getPath("Scripts/bootstrap/js/bootstrap.min.js")));

            bundles.Add(new ScriptBundle("~/js/morrisCharts").Include(
                       getPath("Scripts/morris/morris.min.js")));

            bundles.Add(new ScriptBundle("~/js/sparkline").Include(
                      getPath("Scripts/sparkline/jquery.sparkline.min.js")));

            bundles.Add(new ScriptBundle("~/js/jvectormap").Include(
                      getPath("Scripts/jvectormap/jquery-jvectormap-1.2.2.min.js"),
                      getPath("Scripts/jvectormap/jquery-jvectormap-world-mill-en.js")));

            bundles.Add(new ScriptBundle("~/js/jQueryKnobChart").Include(
                       getPath("Scripts/knob/jquery.knob.js")));

           
            bundles.Add(new ScriptBundle("~/js/dateRangePicker").Include(
                       getPath("Scripts/daterangepicker/daterangepicker.js")));

            bundles.Add(new ScriptBundle("~/js/datePicker").Include(
                       getPath("Scripts/datepicker/bootstrap-datepicker.js")));

            bundles.Add(new ScriptBundle("~/js/boostrapEditor").Include(
                       getPath("Scripts/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js")));

            bundles.Add(new ScriptBundle("~/js/slimScroll").Include(
                      getPath("Scripts/slimScroll/jquery.slimscroll.min.js")));

            bundles.Add(new ScriptBundle("~/js/fastClick").Include(
                      getPath("Scripts/fastclick/fastclick.min.js")));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                      getPath("Scripts/js/app.min.js")));

            //bundles.Add(new ScriptBundle("~/js/morris-charts").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       getPath("Scripts/bootstrap/js/bootstrap.min.js")));

            bundles.Add(new StyleBundle("~/Admin/Content/css").Include(
                      getPath("Scripts/bootstrap/css/bootstrap.min.css"),
                      getPath("Scripts/bootstrap/css/bootstrap.min.css"),
                      getPath("Content/css/AdminLTE.min.css"),
                      getPath("Content/css/skins/_all-skins.min.css") ));

            bundles.Add(new StyleBundle("~/Admin/Content/iCheck").Include(
                     getPath("Scripts/iCheck/flat/blue.css") ));

            bundles.Add(new StyleBundle("~/Admin/Content/morrischart").Include(
                     getPath("Scripts/morris/morris.css")));

            bundles.Add(new StyleBundle("~/Admin/Content/jvectormap").Include(
                     getPath("Scripts/jvectormap/jquery-jvectormap-1.2.2.css")));

            bundles.Add(new StyleBundle("~/Admin/Content/datePicker").Include(
                     getPath("Scripts/datepicker/datepicker3.css")));

            bundles.Add(new StyleBundle("~/Admin/Content/dateRangePicker").Include(
                     getPath("Scripts/daterangepicker/daterangepicker-bs3.css")));

            bundles.Add(new StyleBundle("~/Admin/Content/textEditor").Include(
                     getPath("Scripts/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css")));
        }

        private static string getPath(string relativePath)
        {
            //return $"~/Areas/Admin/{relativePath}";
            return string.Format("~/Areas/Admin/{0}", relativePath);

        }
    }
}
