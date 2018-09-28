using System.Web.Mvc;
using ShootingCompetitionsRequests.App_Start;
using ShootingCompetitionsRequests.Areas.ShootingClub.Models;
using ShootingCompetitionsRequests.Models;
using ShootServ.Controllers;

namespace ShootServ.Areas.ShootingClub.Controllers
{
    public class AddShootingClubController : BaseController
    {
        //
        // GET: /ShootingClub/ShootingClub/
        private readonly ShooterClubModelParams _modelLogic = new ShooterClubModelParams();

        public ActionResult Index()
        {
            var model = new ShooterClubModelParams();
            return View("Index", model);
        }

        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetShootingRangesByRegion(int idRegion)
        {
            var res = _modelLogic.GetShootingRangesByRegion(idRegion);
            var model = new DropDownListModel
            {
                Name = "IdShootingRange",
                Items = res
            };
            return PartialView("DropDownListModel", model);
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
            var model = _modelLogic.GetClubsByRegion(idCountry, idRegion);
            return PartialView("ListShootingClubs", model);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Add(ShooterClubModelParams model)
        {
            var res = _modelLogic.AddShootingClub(model, CurrentUser.Id);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Delete(int idClub)
        {
            var res = _modelLogic.Delete(idClub, CurrentUser.Id);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }
    }
}
