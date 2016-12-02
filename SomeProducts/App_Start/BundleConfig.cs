
using System.Web.Optimization;

namespace SomeProducts
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/janux").Include(
                        "~/janux/js/jquery-ui-1.10.0.custom.min.js",
                        "~/janux/js/jquery-migrate-1.0.0.min.js",
                        "~/janux/js/jquery.ui.touch-punch.js",
                        "~/janux/js/modernizr.js",
                        "~/janux/js/bootstrap.min.js",
                        "~/janux/js/jquery.cookie.js",
                        "~/janux/js/fullcalendar.min.js",
                        "~/janux/js/jquery.dataTables.min.js",
                        "~/janux/js/excanvas.js",
                        "~/janux/js/jquery.flot.js",
                        "~/janux/js/jquery.flot.pie.js",
                        "~/janux/js/jquery.flot.stack.js",
                        "~/janux/js/jquery.flot.resize.min.js",
                        "~/janux/js/jquery.chosen.min.js",
                        "~/janux/js/jquery.uniform.min.js",
                        "~/janux/js/jquery.cleditor.min.js",
                        "~/janux/js/jquery.noty.js",
                        "~/janux/js/jquery.elfinder.min.js",
                        "~/janux/js/jquery.raty.min.js",
                        "~/janux/js/jquery.iphone.toggle.js",
                        "~/janux/js/jquery.uploadify-3.1.min.js",
                        "~/janux/js/jquery.gritter.min.js",
                        "~/janux/js/jquery.imagesloaded.js",
                        "~/janux/js/jquery.masonry.min.js",
                        "~/janux/js/jquery.knob.modified.js",
                        "~/janux/js/jquery.sparkline.min.js",
                        "~/janux/js/counter.js",
                        "~/janux/js/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Stylesheets/Common/Common.css",
                      "~/Stylesheets/Common/Table.css",
                      "~/janux/css/bootstrap.min.css",
                      "~/janux/css/bootstrap-responsive.min.css",
                      "~/janux/css/style.css",
                      "~/janux/css/style-responsive.css"));

            bundles.Add(new ScriptBundle("~/Product/Create/Scripts").Include(
                "~/BowerComponents/jquery-simplecolorpicker/jquery.simplecolorpicker.js",
                "~/Scripts/jquery.tmpl.js",
                "~/Scripts/Common/Utils.js",
                "~/Scripts/Product/ProductControl.js",
                "~/Scripts/Product/ProductImageController.js",
                "~/Scripts/Product/ProductModalWindowControl.js",
                "~/Scripts/Product/RemovingModalWindow.js"));

            bundles.Add(new StyleBundle("~/Product/Create/css").Include(
                "~/Stylesheets/Product/Product.css",
                "~/BowerComponents/jquery-simplecolorpicker/jquery.simplecolorpicker.css"));

            bundles.Add(new StyleBundle("~/Product/ShowProduct/css").Include(
                "~/Stylesheets/Product/ShowProduct.css"));

            bundles.Add(new ScriptBundle("~/Product/ShowProduct/Scripts").Include(
                "~/Scripts/Common/Utils.js",
                "~/Scripts/Product/ProductImageController.js"));

            bundles.Add(new StyleBundle("~/Account/Auth/css").Include(
                "~/Stylesheets/AccountAuth/AccountAuth.css"));

            bundles.Add(new ScriptBundle("~/Account/Auth/Scripts").Include(
                "~/Scripts/Common/Utils.js",
                "~/Scripts/AccountAuth/Validation.js"));

            bundles.Add(new StyleBundle("~/Product/ProductTable/css").Include(
                "~/Stylesheets/ProductTable/ProductTable.css"));

            bundles.Add(new ScriptBundle("~/Product/ProductTable/Scripts").Include(
                "~/Scripts/Common/Utils.js",
                "~/Scripts/ProductTable/RemovingModalWindow.js"));

            bundles.Add(new ScriptBundle("~/Product/Admin/Scripts").Include(
                "~/Scripts/Common/Utils.js",
                "~/Scripts/Admin/UserTableController.js",
                "~/Scripts/Admin/ModalWindow.js"));

            bundles.Add(new StyleBundle("~/Product/Admin/css").Include(
               "~/Stylesheets/Admin/UserTable.css"));

            bundles.Add(new ScriptBundle("~/Product/Common/Filter/Scripts").Include(
                "~/Scripts/Common/Filter/FilterValidation.js",
                "~/Scripts/Common/Filter/FilterController.js"));

            bundles.Add(new StyleBundle("~/Product/Common/Filter/css").Include(
               "~/Stylesheets/Common/Filter.css"));

            bundles.Add(new ScriptBundle("~/Product/Common/Sorting/Scripts").Include(
               "~/Scripts/Common/Sorting/Sorting.js"));

            bundles.Add(new ScriptBundle("~/Product/Audit/Scripts").Include(
                "~/Scripts/Common/Utils.js"));

            bundles.Add(new StyleBundle("~/Product/Audit/css").Include(
               "~/Stylesheets/Audit/AuditTable.css"));

            bundles.Add(new ScriptBundle("~/Product/Admin/Company/Scripts").Include(
                "~/Scripts/Common/Utils.js",
                "~/Scripts/Admin/CompanyController.js"));

            bundles.Add(new StyleBundle("~/Product/Admin/Company/css").Include(
               "~/Stylesheets/Admin/Companies.css"));
        }
    }
}
