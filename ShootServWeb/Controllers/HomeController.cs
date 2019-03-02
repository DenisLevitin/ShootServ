using System.Linq;
using System.Web.Mvc;
using BL;
using BO;
using Serilog;
using ShootServ.Models;

namespace ShootServ.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly ShooterCategoryLogic _shooterCategoryLogic;
        private readonly ShooterLogic _shooterLogic;

        public HomeController(ILogger logger) : base(logger)
        {
            _shooterCategoryLogic = new ShooterCategoryLogic();
            _shooterLogic = new ShooterLogic();
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
            var model = new UserInfoModel(CurrentUser);

            if ( CurrentUser?.IdRole == (int)RolesEnum.Shooter)
            {
                var shooter = _shooterLogic.GetByUser(CurrentUser.Id);
                var shooterCategories = _shooterCategoryLogic.GetAll();
                var weaponTypes = _shooterLogic.GetAllWeaponTypes();
                model.ShooterCategory = shooterCategories.FirstOrDefault(x => x.Id == shooter.IdCategory);
                model.Weapon = weaponTypes.FirstOrDefault(x => x.Id == shooter.IdWeaponType);
            }

            return PartialView("LoginPartial", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания приложения.";
            return View();
        }   
    }
}
