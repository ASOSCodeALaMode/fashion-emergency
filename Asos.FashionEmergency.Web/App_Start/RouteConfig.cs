using System.Web.Mvc;
using System.Web.Routing;

namespace Asos.FashionEmergency.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ViewProducts",
                url: "postcode/{postcode}/{category}",
                defaults: new { controller = "Product", action = "ViewProducts", category = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Product", action = "Index" }
                );
        }
    }
}
