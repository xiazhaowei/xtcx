using System.Web;
using System.Web.Optimization;

namespace xtcx
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/scripts/vendor/jquery/jquery.min.js",
                      "~/Scripts/vendor/bootstrap/js/bootstrap.min.js",
                      "~/scripts/vendor/metisMenu/metisMenu.min.js",
                      "~/scripts/vendor/raphael/raphael.min.js",
                      "~/scripts/vendor/datatables/js/jquery.dataTables.min.js",
                      "~/scripts/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                      "~/scripts/vendor/datatables-responsive/dataTables.responsive.js",
                      "~/content/dist/js/sb-admin-2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/scripts/vendor/bootstrap/css/bootstrap.min.css",
                      "~/scripts/vendor/metisMenu/metisMenu.min.css",
                      "~/content/dist/css/sb-admin-2.css",
                      "~/scripts/vendor/morrisjs/morris.css",
                      "~/scripts/vendor/font-awesome/css/font-awesome.min.css",
                      "~/scripts/vendor/datatables-plugins/dataTables.bootstrap.css",
                      "~/Content/css/mixins.css",
                      "~/Content/admin.css"));
        }
    }
}
