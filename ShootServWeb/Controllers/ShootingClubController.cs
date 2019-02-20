using System.Web.Mvc;
using BL;
using BO;
using Serilog;
using ShootServ.Models;
using ShootServ.Models.ShootingClub;

namespace ShootServ.Controllers
{
    public class ShootingClubController : BaseController
    {
        //
        // GET: /ShootingClub/ShootingClub/
        private readonly ShootingClubLogic _shootingClubLogic;

        public ShootingClubController(ILogger logger) : base(logger)
        {
            _shootingClubLogic = new ShootingClubLogic();
        }

        public ActionResult Index()
        {
            var model = new ShooterClubPageModel()
            {
                Countries = StandartClassifierModelLogic.GetCountryList()
            };
            return View("Index", model);
        }

        /// <summary>
        /// Получить список стрелковых клубов по региону
        /// </summary>
        /// <param name="idCountry">ид. страны</param>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetShootingClubsByRegion(int? idCountry, int? idRegion)
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
            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }
    }
}
