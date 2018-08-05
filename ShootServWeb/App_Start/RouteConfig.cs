using System.Web.Mvc;
using System.Web.Routing;

namespace ShootingCompetitionsRequests
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Registration",
                url : "Registration/{idUser}",
                defaults : new {  controller = "Registration", idUser = UrlParameter.Optional, action = "Index" }
                );
        }
    }
}