using System.Web.Mvc;
using BO;
using Serilog;

namespace ShootServ.Controllers
{
    public class HomeController : BaseController
    {     
        public HomeController(ILogger logger) : base(logger)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">сообщение, которое будет выводиться при загрузге Index страницы</param>
        /// <returns></returns>
        public ActionResult Index(string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }

            return View();
        }

        public ActionResult GetUserInfo()
        {
            var user = new UserParams();
            if (CurrentUser != null)
            {
                user = CurrentUser;
            }

            return PartialView("UserInfo", user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания приложения.";
            return View();
        }   
    }
}
