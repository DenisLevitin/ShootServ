using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using ShootingCompetitionsRequests.Models;
using BL;

namespace ShootingCompetitionsRequests.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Полный список кодов состояния см. по адресу http://go.microsoft.com/fwlink/?LinkID=177550
            //.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Имя пользователя уже существует. Введите другое имя пользователя.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Имя пользователя для данного адреса электронной почты уже существует. Введите другой адрес электронной почты.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Указан недопустимый пароль. Введите допустимое значение пароля.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Указан недопустимый адрес электронной почты. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Указан недопустимый ответ на вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Указан недопустимый вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Указано недопустимое имя пользователя. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.ProviderError:
                    return "Поставщик проверки подлинности вернул ошибку. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                case MembershipCreateStatus.UserRejected:
                    return "Запрос создания пользователя был отменен. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                default:
                    return "Произошла неизвестная ошибка. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";
            }
        }
        #endregion
    }
}
