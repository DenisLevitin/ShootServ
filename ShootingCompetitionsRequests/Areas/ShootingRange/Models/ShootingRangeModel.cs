using BL;
using BO;
using ShootingCompetitionsRequests.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.ShootingRange.Models
{
    /// <summary>
    /// Модель для страницы добавления тира
    /// </summary>
    public class ShootingRangeModelParams
    {
        /// <summary>
        /// Название тира
        /// </summary>
        [DisplayName("Название тира")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес тира
        /// </summary>
        [DisplayName("Адрес")]
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        /// <summary>
        /// Ид. выбранного региона
        /// </summary>
        [DisplayName("Регион")]
        public int RegionId { get; set; }

        /// <summary>
        /// Ид. страны
        /// </summary>
        [DisplayName("Страна")]
        public int CountryId { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [DisplayName("Город")]
        public string Town { get; set; }

        /// <summary>
        /// Название региона
        /// </summary>
        public string RegionName
        {
            get
            {
                var region = new RegionsLogic().Get(RegionId);
                return region != null ? region.Name : "";
            }
        }

        /// <summary>
        /// Идентификатор тира
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Информация о тире
        /// </summary>
        [DisplayName("Информация о тире")]
        public string Info { get; set; }

        /// <summary>
        /// Залогирован ли пользователь в системе
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// Список доступных регионов
        /// </summary>
        public List<SelectListItem> Regions;

        /// <summary>
        /// Список доступных стран
        /// </summary>
        public List<SelectListItem> Countries;

        public ShootingRangeModelParams()
        {
            Regions = new List<SelectListItem>();
            Countries = StandartClassifierModelLogic.GetCountryList().Data;
        }

    }

    public class ShootingRangeModelLogic
    {
        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="regionId">ид. региона</param>
        /// <returns></returns>
        public static List<ShootingRangeModelParams> GetAllByRegion(int regionId)
        {
            var res = new List<ShootingRangeModelParams>();
            
            var blShootingRange = new ShootingRangeLogic();
            var list = blShootingRange.GetByRegion(regionId);

            foreach (var item in list)
            {
                res.Add(new ShootingRangeModelParams
                {
                    Address = item.Address,
                    Id = item.Id,
                    Info = item.Info,
                    Name = item.Name,
                    Phone = item.Phone,
                    RegionId = item.IdRegion,
                    Town = item.Town
                });
            }

            return res;
        }

        /// <summary>
        /// Добавить тир
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        public static ResultInfo Add(ShootingRangeModelParams model, int userId)
        {
            var blShootingRange = new ShootingRangeLogic();

            var shootingRangeParams = new ShootingRangeParams
            {
                Address = model.Address,
                Id = 0,
                IdRegion = model.RegionId,
                Info = model.Info,
                Name = model.Name,
                Phone = model.Phone,
                Town = model.Town,
                IdUser = userId
            };

            return blShootingRange.Add(shootingRangeParams, userId);
        }

        public static ResultInfo Delete(int idShootingRange, int idUser)
        {
            return new ShootingRangeLogic().Delete(idShootingRange, idUser);
        }
    }
}