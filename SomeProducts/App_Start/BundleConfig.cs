﻿
using System.Web.Optimization;

namespace SomeProducts
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

             bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Stylesheets/Common/Common.css"));

            bundles.Add(new ScriptBundle("~/Product/Create/Scripts").Include(
                "~/Scripts/Common/Utils.js",
                "~/Scripts/Product/ProductControl.js",
                "~/Scripts/Product/ProductImageController.js",
                "~/Scripts/Product/ProductModalWindowControl.js",
                "~/Scripts/Product/RemovingModalWindow.js",
                "~/BowerComponents/jquery-simplecolorpicker/jquery.simplecolorpicker.js"));

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
