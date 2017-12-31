using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.Cup
{
    public class CupAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Cup";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Cup_default",
                "Cup/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
