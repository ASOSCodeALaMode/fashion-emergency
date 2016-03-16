using System.Web.Http;

namespace Asos.FashionEmergency.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "OnTheDotApi",
                routeTemplate: "api/{controller}/{action}/{storeId}/{postcode}/{timeslotId}/{uuid}",
                defaults: new { timeslotId = RouteParameter.Optional, uuid = RouteParameter.Optional });
        }
    }
}
