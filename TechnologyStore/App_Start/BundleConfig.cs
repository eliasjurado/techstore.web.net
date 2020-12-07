using System.Web;
using System.Web.Optimization;

namespace TechnologyStore
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/creditly.js",
                      "~/Scripts/creditly2.js",
                      "~/Scripts/easing.js",
                      "~/Scripts/easyResponsiveTabs.js",
                      "~/Scripts/imagezoom.js",
                      "~/Scripts/jquery-2.2.3.min.js",
                      "~/Scripts/jquery.magnific-popup.js",
                      "~/Scripts/jquery.flexslider.js",
                      "~/Scripts/minicart.js",
                      "~/Scripts/scroll.js",
                      "~/Scripts/SmoothScroll.min.js",
                      "~/Scripts/move-top.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/popuo-box.css",
                      "~/Content/menu.css",
                      "~/Content/creditly.css",
                      "~/Content/easy-responsive-tabs.css",
                      "~/Content/flexslider.css",
                      "~/Content/Estilos.css"));

            

        }
    }
}
