using System.Web.Mvc;

namespace ShootServ.Areas.ShootingRange
{
    public class ShootingRangeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ShootingRange";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ShootingRange_default",
                "ShootingRange/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
