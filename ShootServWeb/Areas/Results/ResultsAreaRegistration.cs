using System.Web.Mvc;

namespace ShootServ.Areas.Results
{
    public class ResultsAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Results_default",
                "Results/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

        public override string AreaName
        {
            get { return "Results"; }
        }
    }
}