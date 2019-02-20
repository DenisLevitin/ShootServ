using System.Web.Mvc;
using BL;
using ShootingCompetitionsRequests.Models;

namespace ShootServ.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userLogic = new UserLogic();
                var query = userLogic.Authentification(model.UserName, model.Password);
                if (query.Result.IsOk)
                {
                    // аутентификация произошла успешно
                    Session["user"] = query.Data;
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", query.Result.ErrorMessage);
                }
            }
            else
            {
                ModelState.AddModelError("", "Неккоректно введены логин или пароль");
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
            return View(model);
        }

        /// <summary>
        /// Запрос на восстановление пароля
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="email">мыло</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryRecoveryPassword(string login, string email)
        {
            var userLogic = new UserLogic();
            var res = userLogic.QueryForRecoveryPassword(login, email);

            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult RecoveryPasswordForm()
        {
            return View("RecoverPassword");
        }

        public ActionResult RecoveryPassword(int idUser, int idRec)
        {
            var userLogic = new UserLogic();
            var res = userLogic.RecoveryPassword(idUser, idRec);
            string messageChange = res.IsOk ? "Ваш пароль был изменени. В последствии вы можете его сменить на странице отображения ваших данных" : res.ErrorMessage;

            return RedirectToAction("Index", "Home", new { message = messageChange });
        }

        #region Вспомогательные методы
        private ActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? (ActionResult) Redirect(returnUrl) : RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
