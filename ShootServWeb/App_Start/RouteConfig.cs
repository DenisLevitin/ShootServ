using System.Web.Mvc;
using System.Web.Routing;

namespace ShootServ
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Registration",
                url : "Registration/{action}",
                defaults : new {  controller = "Registration", action = "Index" }
            );

            routes.MapRoute(
                name: "ShootingRange",
                url: "ShootingRange/{action}",
                defaults: new {controller = "ShootingRange", action = "Index"}
            );
            
            routes.MapRoute(
                name: "ShootingClub",
                url: "ShootingClub/{action}",
                defaults: new {controller = "ShootingClub", action = "Index"}
            );
                
            routes.MapRoute(
                name: "Cup",
                url: "Cup/{action}",
                defaults: new { controller = "Cup", action = "Index" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "Profile",
                defaults: new { controller = "Registration", action = "Profile" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );            
        }
    }
}