using System.Collections.Generic;
using System.Web.Mvc;

namespace ShootServ.Models.Registration
{
    public class RegistrationPageModel
    {
        public RegistrationPostModel PostModel { get; set; }
        
        /// <summary>
        /// Происходит ли на странице редактирование
        /// </summary>
        public bool IsEditMode { get; set; }
        
        /// <summary>
        /// Список регионов
        /// </summary>
        public List<SelectListItem> RegionsList { get; set; }

        /// <summary>
        /// Список стрелковых клубов
        /// </summary>
        public List<SelectListItem> ShooterClubs { get; set; }

        /// <summary>
        /// Типы оружия
        /// </summary>
        public List<SelectListItem> WeaponTypes { get; set; }

        /// <summary>
        /// Разряды
        /// </summary>
        public List<SelectListItem> Categories { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public List<SelectListItem> SexList { get; set; }

        /// <summary>
        /// Список ролей
        /// </summary>
        public List<SelectListItem> RolesList { get; set; }

        /// <summary>
        /// Список стран
        /// </summary>
        public List<SelectListItem> CountriesList { get; set; }

        public RegistrationPageModel()
        {
            PostModel = new RegistrationPostModel();
            RegionsList = new List<SelectListItem>();
            ShooterClubs = new List<SelectListItem>();
            WeaponTypes = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            SexList = new List<SelectListItem>();
            RolesList = new List<SelectListItem>();
            CountriesList = new List<SelectListItem>();
        }
    }
}