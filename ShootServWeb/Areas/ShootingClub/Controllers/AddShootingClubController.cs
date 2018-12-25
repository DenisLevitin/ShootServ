using System.Linq;
using System.Web.Mvc;
using BL;
using BO;
using ShootServ.Areas.ShootingClub.Models;
using ShootServ.Controllers;

namespace ShootServ.Areas.ShootingClub.Controllers
{
    public class AddShootingClubController : BaseController
    {
        //
        // GET: /ShootingClub/ShootingClub/
        private readonly ShootingClubLogic _shootingClubLogic;

        public AddShootingClubController()
        {
            _shootingClubLogic = new ShootingClubLogic();
        }

        public ActionResult Index()
        {
            var model = new ShooterClubModelParams(); /// TODO: Другая модель для вьюхи нужна
            return View("Index", model);
        }

        /// <summary>
        /// Получить список стрелковых клубов по региону
        /// </summary>
        /// <param name="idCountry">ид. страны</param>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetShootingClubsByRegion(int idCountry=-1, int idRegion=-1)
        {
            var list = _shootingClubLogic.GetByRegion(idCountry, idRegion);       
            return new JsonResult{ Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Add(ShooterClubModelParams model)
        {
            var shootingClub = new ShooterClubParams
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                MainCoach = model.MainCoach,
                IdShootingRange = model.IdShootingRange,
                Phone = model.Phone,
                CreatorId = CurrentUser.Id
            };
            var res = _shootingClubLogic.Add(shootingClub, CurrentUser.Id);
            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Delete(int idClub)
        {
            var res = _shootingClubLogic.Delete(idClub, CurrentUser.Id);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }
    }
}
