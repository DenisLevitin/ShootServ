using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина названия должна быть от 10 до 100 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес тира
        /// </summary>
        [DisplayName("Адрес")]
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина адреса должна быть от 20 до 200 символов")]
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [DisplayName("Телефон")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "Длина адреса должна быть от 7 до 12 символов")]
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
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина названия города должна быть от 3 до 25 символов")]
        public string Town { get; set; }

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
            Countries = StandartClassifierModelLogic.GetCountryList();
            Regions = StandartClassifierModelLogic.GetRegionsByCountry(1); /// TODO : HardCode
        }

    }
}