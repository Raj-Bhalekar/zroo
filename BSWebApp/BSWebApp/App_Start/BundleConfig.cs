using System.Web;
using System.Web.Optimization;

namespace BSWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerybs").Include(
                      "~/Scripts/jquery.timepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerygrd").Include(
                     "~/Scripts/jquery.jqgrid.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(

                     // "~/Content/site.css"
                      // "~/Content/themes/base/tabs.css",
                      "~/Content/ui.jqgrid.min.css"
                     
                       , "~/Content/themes/base/jquery-ui.min.css"
                      , "~/Content/themes/base/minified/jquery.timepicker.css"
                      //,"~/Content/bootstrap.css"

                      ));


            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //       "~/Content/bootstrap.css",
            //       "~/Content/site.css"
            //       , "~/Content/themes/base/minified/jquery.timepicker.css"
            //       ));
        }
    }
}
