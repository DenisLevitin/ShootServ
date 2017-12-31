using BL;
using BO;
using ShootingCompetitionsRequests.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserLogic _userLogic;

        public HomeController()
        {
            _userLogic = new UserLogic();
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

        //[CustomAuthorize]
        public ActionResult GetUserInfo()
        {
            var user = new UserParams();
            var sessionUser = Session["user"];
            if (sessionUser != null)
            {
                user = sessionUser as UserParams;
            }

            return PartialView("UserInfo", user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания приложения.";

            return View();
        }

        [CustomAuthorize]
        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect(Url.Action("Login", "Account"));
        }
    }
}
