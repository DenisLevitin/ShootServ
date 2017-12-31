using ShootingCompetitionsRequests.Areas.ShootingClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShootingCompetitionsRequests.Models;
using ShootingCompetitionsRequests.App_Start;
using BO;

namespace ShootingCompetitionsRequests.Areas.ShootingClub.Controllers
{
    public class AddShootingClubController : Controller
    {
        //
        // GET: /ShootingClub/ShootingClub/
        private readonly ShooterClubModelParams _modelLogic = new ShooterClubModelParams();

        //[CustomAuthorize]
        public ActionResult Index()
        {
            var model = new ShooterClubModelParams();
            model.IsLogin = Session["user"] != null;

            return View("Index", model);
        }

        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        [HttpGet]
        //[CustomAuthorize]
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
        //[CustomAuthorize]
        public ActionResult GetShootingClubsByRegion(int idCountry=-1, int idRegion=-1)
        {
            var model = _modelLogic.GetClubsByRegion(idCountry, idRegion);
            return PartialView("ListShootingClubs", model);
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Add(ShooterClubModelParams model)
        {
            var res = _modelLogic.AddShootingClub(model, _user.Id);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult Delete(int idClub)
        {
            var res = _modelLogic.Delete(idClub, _user.Id);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }

        private UserParams _user
        {
            get
            {
                if (Session["user"] != null)
                {
                    return (UserParams)Session["user"];
                }
                else return null;
            }
        }
    }
}
