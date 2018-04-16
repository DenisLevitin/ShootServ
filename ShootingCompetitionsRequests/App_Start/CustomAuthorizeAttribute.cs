using System.Web;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.App_Start
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["user"] != null;
        }
    }
}