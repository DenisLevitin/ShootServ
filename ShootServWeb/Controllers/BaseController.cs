using System.Web.Mvc;
using BO;
using Serilog;

namespace ShootServ.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger _logger;
        
        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
        
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
            _logger.Error(filterContext.Exception, filterContext.Exception.Message);
        }
    }
}