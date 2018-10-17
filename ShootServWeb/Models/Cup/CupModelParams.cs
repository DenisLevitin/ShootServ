using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace ShootServ.Models.Cup
{
    /// <summary>
    /// Модель соревнования
    /// </summary>
    public class CupModelParams
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        /// Ид. типа кубка
        /// </summary>
        [DisplayName("Масштаб соревнования")]
        public int IdCupType { get; set; }

        /// <summary>
        /// Дата начала кубка
        /// </summary>
        [DisplayName("Дата начала соревнования")]
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Дата окончания кубка
        /// </summary>
        [DisplayName("Дата окончания соревнования")]
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        [DisplayName("Страна")]
        public int IdCountry { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        [DisplayName("Регион")]
        public int IdRegion { get; set; }

        /// <summary>
        /// Тир
        /// </summary>
        [DisplayName("Тир")]
        public int IdShootingRange { get; set; }

        /// <summary>
        /// Положение о соревновании
        /// </summary>
        public byte[] Document { get; set; }

        /// <summary>
        /// Ид. юзера, создавшего соревнование
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Дата создания соревнования
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Упражнения на соревновании
        /// </summary>
        public List<CompetitionModelParams> CompetitionTypes { get; set; }

        /// <summary>
        /// Список регионов
        /// </summary>
        public List<SelectListItem> Regions { get; set; }

        /// <summary>
        /// Список тиров
        /// </summary>
        public List<SelectListItem> ShootingRanges { get; set; }

        /// <summary>
        /// Список типов соревнований
        /// </summary>
        public List<SelectListItem> CupTypes { get; set; }

        /// <summary>
        /// Список стран
        /// </summary>
        public List<SelectListItem> Countries { get; set; }

        /// <summary>
        /// Находится ли модель в режиме редактирования
        /// </summary>
        public bool IsEditMode { get; set; }

        public CupModelParams()
        {
            CompetitionTypes = new List<CompetitionModelParams>();
            Regions = new List<SelectListItem>();
            ShootingRanges = new List<SelectListItem>();
            CupTypes = new List<SelectListItem>();
            Countries = new List<SelectListItem>();

            DateStart = DateTime.Now.AddDays(10);
            DateEnd = DateTime.Now.AddDays(12);

            IsEditMode = false;
        }
    }
}