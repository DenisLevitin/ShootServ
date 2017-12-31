using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.ShootingClub
{
    public class ShootingClubAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ShootingClub";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ShootingClub_default",
                "ShootingClub/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
