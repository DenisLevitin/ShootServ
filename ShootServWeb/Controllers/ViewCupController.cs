﻿using System.Web.Mvc;
using BL;
using BO;
using Serilog;
using ShootServ.Models.Cup;

namespace ShootServ.Controllers
{
    public class ViewCupController : BaseController
    {
        //
        // GET: /Cup/ViewCup/
        private readonly ViewCupModelLogic _viewCupModelLogic;
        private readonly EntryForCompetitionsLogic _entryLogic;
        private readonly CupLogic _cupLogic;
        
        public ViewCupController(ILogger logger) : base(logger)
        {
            _viewCupModelLogic = new ViewCupModelLogic();
            _entryLogic = new EntryForCompetitionsLogic();
            _cupLogic = new CupLogic();
        }

        public ActionResult Index(int idCup)
        {
            var model = _cupLogic.GetDetailsCup(idCup);
            return View(model);
        }

        public ActionResult GetCompetitionsList(int idCup)
        {
            int idUser = CurrentUser?.Id ?? -1;
            var competitions = _viewCupModelLogic.GetCompetitionList(idCup, idUser);

            var model = new ViewCupCompetitionModel
            {
                Competitions = competitions,
                ShowEntryButton = (CurrentUser != null && CupModelLogic.IsUserShooter(CurrentUser)),
                IdCup = idCup
            };

            return PartialView("CompetitionsList", model);
        }

        public ActionResult GetEntryShootersList(int idCup)
        {            
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
            return query.Result.IsOk ? (ActionResult) File(query.Data.Content, System.Net.Mime.MediaTypeNames.Application.Octet, query.Data.FileName) : new EmptyResult();
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

            if (CurrentUser != null)
            {
                res = _viewCupModelLogic.CreateEntry(CurrentUser.Id, idCup, idCompType);
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
