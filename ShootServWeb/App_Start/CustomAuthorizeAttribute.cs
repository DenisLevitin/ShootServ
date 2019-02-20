using System.Web.Mvc;

namespace ShootServ
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(401);
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
        }
    }
}