﻿using System.Web;
using System.Web.Optimization;

namespace MeetUp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/lightslider.min.css",
                      "~/Content/toastr.css"
                      ));

            //bundles.Add(new StyleBundle("~/Scripts/js").Include(
            //          "~/Scripts/js/ImageUpload.js"
            //          ));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                       "~/Scripts/toastr.js*"
                      ));

            bundles.Add(new ScriptBundle("~/Scripts/js")
                .IncludeDirectory("~/Scripts/js", "*.js", true));


            bundles.Add(new StyleBundle("~/Scripts/lightslider.js").Include(
                      "~/Scripts/js/lightslider.min.js"
                      ));
        }
    }
}
