using System.Web;
using System.Web.Mvc;

namespace ShootServ
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["user"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.RequestContext.HttpContext.Response.StatusCode = 401;
            }
        }
    }
}