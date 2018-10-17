using System.Collections.Generic;
using System.Web.Mvc;
using BL;
using BO;

namespace ShootServ.Models.Cup
{
    public class ViewCupModelLogic
    {
        private readonly EntryForCompetitionsLogic _entryLogic;
        private readonly ShooterLogic _shooterLogic;
        private readonly ShootingClubLogic _clubLogic;
        private readonly CompetitionTypeLogic _competitionTypeLogic;

        public ViewCupModelLogic()
        {
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