using System.Web.Mvc;
using BO;

namespace ShootServ.Controllers
{
    public class BaseController : Controller
    {
        public UserParams CurrentUser
        {
            get { return Session["user"] as UserParams; }
        }

        public bool IsLogin
        {
            get { return CurrentUser != null; }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.IsLogin = IsLogin;
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // logger
        }
    }
}