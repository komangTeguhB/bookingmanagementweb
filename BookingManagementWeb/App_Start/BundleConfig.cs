using System.Web;
using System.Web.Optimization;

namespace BookingManagementWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/App/Common/ApiUrl.js",
                      "~/App/Controller/ClientController.js",
                      "~/App/Controller/AuthController.js",
                      "~/App/Controller/StaffController.js",
                      "~/App/Controller/ConsultationTypeController.js",
                      "~/App/Controller/StaffSessionController.js",
                      "~/App/Controller/SessionsBookedController.js",
                      "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/CustomStyle.css"));
            
        }
    }
}
