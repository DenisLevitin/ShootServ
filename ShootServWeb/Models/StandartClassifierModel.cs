using BL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BO;

namespace ShootingCompetitionsRequests.Models
{
    /// <summary>
    /// Стандартная логика получения моделей для справочников
    /// </summary>
    public class StandartClassifierModelLogic
    {
        /// <summary>
        /// Получить список типов оружия
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetWeaponTypeList()
        {
            return new List<SelectListItem> { new SelectListItem { Value = ((int)BO.WeaponTypeParams.WeaponTypeEnum.Rifle).ToString(), Text = "Винтовка"},
                                              new SelectListItem { Value = ((int)BO.WeaponTypeParams.WeaponTypeEnum.Pistol).ToString(), Text = "Пистолет"},
                                              new SelectListItem { Value = ((int)BO.WeaponTypeParams.WeaponTypeEnum.RifleMovingTarget).ToString(), Text = "Винтовка, движущаяся мишень"}  };
        }

        /// <summary>
        /// Получить список полов
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetSexList()
        {
            return new List<SelectListItem> { new SelectListItem { Value = ((int)BO.SexEnum.Men).ToString(), Text = "Мужской"},
                                              new SelectListItem { Value = ((int)BO.SexEnum.Women).ToString(), Text = "Женский"} };
        }

        /// <summary>
        /// Получить список ролей
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetRolesList()
        {
            /// TODO: Возможно переделать на запрос из базы

            return new List<SelectListItem> { new SelectListItem { Value = ((int)BO.RolesEnum.Organization).ToString(), Text = "Организатор"},
                                              new SelectListItem { Value = ((int)BO.RolesEnum.Shooter).ToString(), Text = "Стрелок"} };
        }

        /// <summary>
        /// Получить список регионов
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetRegionsByCountry(int idCountry, bool addAll = true)
        {
            var res = new List<SelectListItem>();
            if (addAll)
                res.Add(new SelectListItem { Value = "-1", Text = "Все регионы" });

            var query = new RegionsLogic().GetByCountry(idCountry).Data;
            foreach (var item in query)
            {
                res.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }

            return res;
        }

        /// <summary>
        /// Получить список регионов
        /// </summary>
        /// <returns></returns>
        public static ResultInfoRef<List<SelectListItem>> GetCountryList(bool addAll = true)
        {
            var res = new ResultInfoRef<List<SelectListItem>>();

            var countries = new CountryLogic().GetAllCounties();
            if (addAll)
            {
                res.Data.Add(new SelectListItem { Value = "-1", Text = "Все страны" });
            }

            foreach (var item in countries)
            {
                res.Data.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.CountryName });
            }

            if (res.Data.Any(x => x.Value == "1"))
            {
                res.Data.Single(x => x.Value == "1").Selected = true;
            }

            return res;
        }
    }
}