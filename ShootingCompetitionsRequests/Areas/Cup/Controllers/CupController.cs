using BO;
using Newtonsoft.Json;
using ShootingCompetitionsRequests.App_Start;
using ShootingCompetitionsRequests.Areas.Cup.Models;
using ShootingCompetitionsRequests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.Cup.Controllers
{
    public class CupController : Controller
    {
        //
        // GET: /Cup/Cup/
        private readonly CupModelLogic _modelLogic;

        public CupController()
        {
            _modelLogic = new CupModelLogic();
        }

        //[CustomAuthorize]
        public ActionResult Index(int idCup = -1)
        {
            var model = _modelLogic.GetModelForIndex(idCup);
            model.IsLogin = Session["user"] != null;
            return View("Index", model);
        }

        //[CustomAuthorize]
        public ActionResult GetShootingRanges(int idRegion)
        {
            var query = _modelLogic.GetShootingRangesByRegion(idRegion);

            return PartialView("DropDownListModel", new DropDownListModel { Name = "IdShootingRange", Items = query });
        }

        //[CustomAuthorize]
        public ActionResult GetCupLists(int idRegion = -1, DateTime dateFrom = default(DateTime), DateTime dateTo = default(DateTime))
        {
            var model = _modelLogic.GetListCupsByRegionAndDates(idRegion, dateFrom, dateTo);

            return PartialView("CupsList", model);
        }

        /// <summary>
        /// Добавить соревнование
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult AddCup(CupModelParams model, string competitionTypes)
        {
            var listCompetitions = JsonConvert.DeserializeObject<CompetitionModelParams[]>(competitionTypes);
            var user = (UserParams) Session["user"];
            var res = _modelLogic.AddCup(model, listCompetitions.ToList(), user.Id);

            return new JsonResult { JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet, Data = new { IsOk = res.Result.IsOk, Message = res.Result.ErrorMessage, IdCup = res.Data } };
        }

        [CustomAuthorize]
        public ActionResult DeleteCup(int idCup)
        {
            var user = (UserParams)Session["user"];
            var res = _modelLogic.DeleteCup(idCup, user.Id);

            return new JsonResult { JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet, Data = new { IsOk = res.IsOk, Message = res.ErrorMessage } };
        }

        /// <summary>
        /// Обновить соревнование
        /// </summary>
        /// <param name="idEditCup">ид. соревнованиея</param>
        /// <param name="cupModel">соревнование</param>
        /// <param name="competitionTypes">список упражнений</param>
        [HttpGet]
        public ActionResult UpdateCup(int idEditCup, CupModelParams cupModel, string competitionTypes)
        {
            var user = (UserParams)Session["user"];
            var listCompetitions = JsonConvert.DeserializeObject<CompetitionModelParams[]>(competitionTypes);

            var res = _modelLogic.Update(idEditCup, user.Id, cupModel, listCompetitions.ToList());
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { IsOk = res.IsOk, Message = res.ErrorMessage }
            };
        }

    }
}
