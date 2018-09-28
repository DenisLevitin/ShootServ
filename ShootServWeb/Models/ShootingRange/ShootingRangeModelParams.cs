using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using BL;
using ShootingCompetitionsRequests.Models;

namespace ShootServ.Models.ShootingRange
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
        /// Список доступных регионов
        /// </summary>
        public List<SelectListItem> Regions { get; set; }

        /// <summary>
        /// Список доступных стран
        /// </summary>
        public List<SelectListItem> Countries { get; set; }

        public ShootingRangeModelParams()
        {
            Regions = new List<SelectListItem>();
            Countries = StandartClassifierModelLogic.GetCountryList().Data;
        }

    }
}