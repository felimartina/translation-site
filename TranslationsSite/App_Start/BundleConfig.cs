using System.Web;
using System.Web.Optimization;

namespace TranslationsSite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new StyleBundle("~/content/startbootstrap").Include(
                        "~/Content/themes/startbootstrap-modern-business-1.0.3/css/bootstrap.min.css",
                        "~/Content/themes/startbootstrap-modern-business-1.0.3/css/modern-business.css",
                        "~/Content/themes/startbootstrap-modern-business-1.0.3/font-awesome/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/startbootstrap").Include(
                        "~/Content/themes/startbootstrap-modern-business-1.0.3/js/jquery.js",
                        "~/Content/themes/startbootstrap-modern-business-1.0.3/js/bootstrap.min.js"));
            
        }
    }
}