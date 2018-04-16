using BL;
using BO;
using ShootingCompetitionsRequests.App_Start;
using ShootingCompetitionsRequests.Areas.Cup.Models;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.Cup.Controllers
{
    public class ViewCupController : Controller
    {
        //
        // GET: /Cup/ViewCup/

        private readonly CupModelLogic _modelLogic;
        private readonly ViewCupModelLogic _viewCupModelLogic;
        private readonly EntryForCompetitionsLogic _entryLogic;

        public ViewCupController()
        {
            _modelLogic = new CupModelLogic();
            _viewCupModelLogic = new ViewCupModelLogic();
            _entryLogic = new EntryForCompetitionsLogic();
        }

        public ActionResult Index(int idCup)
        {
            var model = _modelLogic.GetCup(idCup);

            ViewBag.IsLogin = Session["user"] != null;
            return View(model);
        }

        public ActionResult GetCompetitionsList(int idCup)
        {
            var user = (UserParams)Session["user"];
            int idUser = user != null ? user.Id : -1;
            var competitions = _viewCupModelLogic.GetCompetitionList(idCup, idUser);

            var model = new ViewCupCompetitionModel
            {
                Competitions = competitions,
                ShowEntryButton = (user != null && _modelLogic.IsUserShooter(user)),
                IdCup = idCup
            };

            return PartialView("CompetitionsList", model);
        }

        public ActionResult GetEntryShootersList(int idCup)
        {
            var clubs = _viewCupModelLogic.GetClubsByCup(idCup);
            
            var model = new EntryShootersModel
            {
                Shooters = _viewCupModelLogic.GetEntryShooters(idCup, -1),
                Clubs = _viewCupModelLogic.GetClubsByCup(idCup)
            };

            return PartialView("EntryShootersList", model);
        }

        public ActionResult PrintEntry(int idCup, int sex, int idClub = -1)
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory+"Content\\Template\\Шаблон_заявки_excel.xlsx";

            var sexEnum = sex == 1 ? SexEnum.Men : SexEnum.Women;
            var query = _entryLogic.PrintEntryList(path, idCup, sexEnum, idClub);
            if (query.Result.IsOk)
            {
                return File(query.Data.Content, System.Net.Mime.MediaTypeNames.Application.Octet, query.Data.FileName);
            }
            else return new EmptyResult();
        }

        public ActionResult GetEntryShootersListByClub(int idCup, int idClub)
        {
            var shooters = _viewCupModelLogic.GetEntryShooters(idCup, idClub);
            return PartialView("ShootersListTable", shooters);
        }

        [CustomAuthorize]
        public ActionResult CreateEntry(int idCompType, int idCup)
        {
            var res = new ResultInfo();
            var user = Session["user"];

            if (user != null && user is UserParams)
            {
                res = _viewCupModelLogic.CreateEntry(((UserParams)user).Id, idCup, idCompType);
            }
            else
            {
                res.IsOk = false;
                res.ErrorMessage = "Истекла сессия пользователя";
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { IsOk = res.IsOk, Message = res.ErrorMessage}
            };
        }

    }
}
