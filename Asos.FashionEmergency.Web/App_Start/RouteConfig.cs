using System.Web.Http.Routing.Constraints;
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
                url: "postcode/{postcode}/{floor}/{category}",
                defaults: new { controller = "Product", action = "ViewProducts", category = UrlParameter.Optional },
                constraints: new { floor = @"^(?!product).*$" }
                );

            routes.MapRoute(
                name: "ViewProduct",
                url: "postcode/{postcode}/product/{productId}",
                defaults: new { controller = "Product", action = "ViewProduct" }
                );

            routes.MapRoute(
                name: "BuyProduct",
                url: "postcode/{postcode}/product/{productId}/buy",
                defaults: new { controller = "Product", action = "BuyProduct" }
                );

            routes.MapRoute(
                name: "OrderComplete",
                url: "postcode/{postcode}/product/{productId}/complete",
                defaults: new { controller = "Product", action = "OrderComplete" }
                );
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Product", action = "Index" }
                );
        }
    }
}
