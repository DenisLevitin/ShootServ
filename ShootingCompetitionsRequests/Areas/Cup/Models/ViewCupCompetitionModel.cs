using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL;
using BO;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.Cup.Models
{
    /// <summary>
    /// Модель для детализации упражнения на странице просмотра соревнований
    /// </summary>
    public class ViewCupShooterCompetitionModel
    {
        /// <summary>
        /// Ид. упражнения на соревновании
        /// </summary>
        public int IdCupCompetitionType { get; set; }

        /// <summary>
        /// Ид. типа упражнения
        /// </summary>
        public int IdCompetitionType { get; set; }

        /// <summary>
        /// Название упражнения
        /// </summary>
        public string NameCompetition { get; set; }

        /// <summary>
        /// Время первой смены
        /// </summary>
        public DateTime TimeFirstShift { get; set; }

        /// <summary>
        /// Был ли стрелок заявлен на упражнение
        /// </summary>
        public bool IsShooterWasEntried { get; set; }
    }

    /// <summary>
    /// Модель для просмотра упражнений на странице просмотра соревнований
    /// </summary>
    public class ViewCupCompetitionModel
    {
        /// <summary>
        /// Список упражнений
        /// </summary>
        public List<ViewCupShooterCompetitionModel> Competitions { get; set; }

        /// <summary>
        /// Показывать ли кнопку добавления на соревнования
        /// </summary>
        public bool ShowEntryButton { get; set; }

        /// <summary>
        /// Ид. соревнования
        /// </summary>
        public int IdCup { get; set; }
    }

    /// <summary>
    /// Модель для отображения завляенных стрелков на соревновании
    /// </summary>
    public class EntryShootersModel
    {
        /// <summary>
        /// Список стрелков
        /// </summary>
        public List<ShooterEntryDetailsParams> Shooters { get; set; }

        /// <summary>
        /// Список стрелковых команд
        /// </summary>
        public List<SelectListItem> Clubs { get; set; }
             
    }

    public class ViewCupModelLogic
    {
        private readonly UserLogic _userLogic;
        private readonly EntryForCompetitionsLogic _entryLogic;
        private readonly ShooterLogic _shooterLogic;
        private readonly ShootingClubLogic _clubLogic;
        private readonly CompetitionTypeLogic _competitionTypeLogic;

        public ViewCupModelLogic()
        {
            _userLogic = new UserLogic();
            _entryLogic = new EntryForCompetitionsLogic();
            _clubLogic = new ShootingClubLogic();
            _shooterLogic = new ShooterLogic();
            _competitionTypeLogic = new CompetitionTypeLogic();
        }

        /// <summary>
        /// Получить список упражнений на соревновании с информацией о состоянии заявки для стрелка
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idUser">ид. юзера</param>
        /// <returns></returns>
        public List<ViewCupShooterCompetitionModel> GetCompetitionList(int idCup, int idUser = -1)
        {
            var res = new List<ViewCupShooterCompetitionModel>();
            var query = _competitionTypeLogic.GetCupCompetitionListWithShooterEntryDetails(idCup, idUser);

            foreach (var item in query)
            {
                res.Add(new ViewCupShooterCompetitionModel
                {
                    IdCupCompetitionType = item.IdCupCompetitionType,
                    IdCompetitionType = item.IdCompetitionType,
                    IsShooterWasEntried = item.IsShooterWasEntried,
                    NameCompetition = item.NameCompetition,
                    TimeFirstShift = item.TimeFirstShift
                });
            }

            return res;
        }

        /// <summary>
        /// Добавить заявку
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idCompetitionType">ид. типа упражнения</param>
        /// <returns></returns>
        public ResultInfo CreateEntry(int idUser, int idCup, int idCompetitionType)
        {
            return _entryLogic.CreateEntry(idUser, idCup, idCompetitionType);
        }

        /// <summary>
        /// Получить список заявленных стрелков
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idClub">ид. команды</param>
        public List<ShooterEntryDetailsParams> GetEntryShooters(int idCup, int idClub)
        {
            return idClub != -1 ? _shooterLogic.GetEntryShootersOnCupAndClub(idCup, idClub)
                :
                _shooterLogic.GetEntryShootersOnCup(idCup);
        }

        /// <summary>
        /// Получить список команд на соревновании
        /// </summary>
        /// <param name="idCup"></param>
        /// <returns></returns>
        public List<SelectListItem> GetClubsByCup(int idCup)
        {
            var clubs = _clubLogic.GetByCup(idCup);

            var res = new List<SelectListItem>();
            res.Add(new SelectListItem { Value = "-1", Text = "Все" });

            foreach (var club in clubs)
            {
                res.Add(new SelectListItem { Value = club.Id.ToString(), Text = club.Name });
            }

            return res;
        }
    }
}