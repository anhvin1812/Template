using System.Web.Optimization;

namespace App.Website.Areas.Admin.App_Start
{
    internal class AdminBundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Admin/Js/jquery").Include(
                        getPath("Scripts/jQuery/jQuery-2.1.4.min.js"),
                        getPath("Scripts/bootstrap/js/bootstrap.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/jqueryUI").Include(
                        getPath("Scripts/jQueryUI/jquery-ui.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/jqueryval").Include(
                getPath("Scripts/jQuery-validate/jquery.validate*"),
                getPath("Scripts/jQuery-validate/bootstrap-style-validation.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/morrisCharts").Include(
                       getPath("Scripts/morris/morris.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/sparkline").Include(
                      getPath("Scripts/sparkline/jquery.sparkline.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/jvectormap").Include(
                      getPath("Scripts/jvectormap/jquery-jvectormap-1.2.2.min.js"),
                      getPath("Scripts/jvectormap/jquery-jvectormap-world-mill-en.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/jQueryKnobChart").Include(
                       getPath("Scripts/knob/jquery.knob.js")));

           
            bundles.Add(new ScriptBundle("~/Admin/Js/dateRangePicker").Include(
                       getPath("Scripts/daterangepicker/daterangepicker.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/datePicker").Include(
                       getPath("Scripts/datepicker/bootstrap-datepicker.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/boostrapEditor").Include(
                       getPath("Scripts/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/slimScroll").Include(
                      getPath("Scripts/slimScroll/jquery.slimscroll.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/fastClick").Include(
                      getPath("Scripts/fastclick/fastclick.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/iCheck").Include(
                      getPath("Scripts/iCheck/icheck.min.js"),
                      getPath("Scripts/js/active-iCheck.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/select2").Include(
                      getPath("Scripts/select2/select2.full.min.js"),
                      getPath("Scripts/App/select2.js")));

            bundles.Add(new ScriptBundle("~/Admin/Js/app").Include(
                      getPath("Scripts/js/app.min.js"),
                      getPath("Scripts/App/common.js"),
                      getPath("Scripts/App/modal.js")
                      ));

            bundles.Add(new ScriptBundle("~/Admin/Js/dataTable").Include(
                      getPath("Scripts/datatables/jquery.dataTables.min.js"),
                      getPath("Scripts/datatables/dataTables.bootstrap.min.js")));

            bundles.Add(new ScriptBundle("~/Admin/App/product").Include(
                      getPath("Scripts/App/product.js")));

            //bundles.Add(new ScriptBundle("~/js/morris-charts").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       getPath("Scripts/bootstrap/js/bootstrap.min.js")));

            bundles.Add(new StyleBundle("~/Admin/Content/css").Include(
                      getPath("Scripts/jQueryUI/jquery-ui.min.css"),
                      getPath("Scripts/bootstrap/css/bootstrap.min.css"),
                      getPath("Scripts/select2/select2.min.css"),
                      getPath("Content/css/AdminLTE.min.css"),
                      getPath("Content/css/skins/_all-skins.min.css"),
                      getPath("Content/css/site.css")));

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

            bundles.Add(new StyleBundle("~/Admin/Css/DataTable").Include(
                     getPath("Scripts/datatables/dataTables.bootstrap.css")));
        }

        private static string getPath(string relativePath)
        {
            return $"~/Areas/Admin/{relativePath}";

        }
    }
}
