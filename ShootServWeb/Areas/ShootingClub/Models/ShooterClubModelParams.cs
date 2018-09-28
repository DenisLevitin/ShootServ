using BL;
using BO;
using ShootingCompetitionsRequests.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.ShootingClub.Models
{
    /// <summary>
    /// Модель стрелкового клуба для добавления
    /// </summary>
    public class ShooterClubModelParams
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название стрелкового клуба
        /// </summary>
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес стрелкового клуба
        /// </summary>
        [DisplayName("Адрес")]
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        /// <summary>
        /// Ид. тира
        /// </summary>
        [DisplayName("Тир")]
        public int IdShootingRange { get; set; }

        /// <summary>
        /// Главный тренер
        /// </summary>
        [DisplayName("Главный тренер")]
        public string MainCoach { get; set; }

        [DisplayName("Регион")]
        public string RegionName { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public ErrorModelParams Error { get; set; }

        /// <summary>
        /// Список тиров
        /// </summary>
        public List<SelectListItem> ShootingRange { get; set; }

        /// <summary>
        /// Страны
        /// </summary>
        public List<SelectListItem> Countries { get; set; }

        /// <summary>
        /// Регионы
        /// </summary>
        public List<SelectListItem> Regions { get; set; }

        private readonly ShootingClubLogic _shootingClubLogic;
        private readonly ShootingRangeLogic _shootingRangeLogic;
        private readonly RegionsLogic _regionsLogic;

        public ShooterClubModelParams()
        {
            _shootingClubLogic = new ShootingClubLogic();
            _shootingRangeLogic = new ShootingRangeLogic();
            _regionsLogic = new RegionsLogic();

            ShootingRange = new List<SelectListItem>();
            Regions = new List<SelectListItem>();
            
            var queryCountries = StandartClassifierModelLogic.GetCountryList();
            if (queryCountries.Result.IsOk)
            {
                Error = new ErrorModelParams();
                Countries = queryCountries.Data;
            }
            else
            {
                Countries = new List<SelectListItem>();
                Error = new ErrorModelParams(queryCountries.Result);
            }
        }

        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public List<SelectListItem> GetShootingRangesByRegion(int idRegion)
        {
            return _shootingRangeLogic.GetByRegion(idRegion).ConvertAll(x => new SelectListItem { Text = string.Format("{0} {1}", x.Town, x.Name), Value = x.Id.ToString() });
        }

        /// <summary>
        /// Получить список стрелковых клубов по региону
        /// </summary>
        /// <param name="idCountry">ид. страны</param>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public List<ShooterClubModelParams> GetClubsByRegion(int idCountry, int idRegion)
        {
            var resBl = _shootingClubLogic.GetByRegion(idCountry, idRegion);
            var res = new List<ShooterClubModelParams>();

            foreach (var item in resBl)
            {
                res.Add(new ShooterClubModelParams
                {
                    Id = item.Club.Id,
                    Address = item.Club.Address,
                    IdShootingRange = item.Club.IdShootingRange,
                    Name = item.Club.Name,
                    Phone = item.Club.Phone,
                    MainCoach = item.Club.MainCoach,
                    RegionName = item.RegionName
                });
            }

            return res;
        }

        /// <summary>
        /// Удалить стрелковый клуб
        /// </summary>
        /// <param name="idClub"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public ResultInfo Delete(int idClub, int idUser)
        {
            return _shootingClubLogic.Delete(idClub, idUser);
        }

        /// <summary>
        /// Добавить стрелковый клуб
        /// </summary>
        /// <param name="club">клуб</param>
        public ResultInfo AddShootingClub(ShooterClubModelParams club, int idUser)
        {
            var blShooterClub = new ShooterClubParams 
            {
                Id = club.Id,
                Address = club.Address,
                IdShootingRange = club.IdShootingRange,
                MainCoach = club.MainCoach,
                Name = club.Name,
                Phone = club.Phone,
                UsId = idUser
            };

            return _shootingClubLogic.Add(blShooterClub);
        }

    }
}