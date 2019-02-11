using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BO;

namespace ShootServ.Models.Registration
{
    /// <summary>
    /// Модель для страницы регистрации
    /// </summary>
    public class RegistrationPostModel
    {
        /// <summary>
        /// Ид. для уже существующего пользователя
        /// </summary>
        public int IdExistingUser { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [DisplayName("Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [DisplayName("Фамилия")]
        public string Family { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DisplayName("Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DisplayName("Отчество")]
        public string FatherName { get; set; }

        /// <summary>
        /// Идентификатор выбранной роли
        /// </summary>
        [DisplayName("Роль")]
        public int IdRole { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Электронная почта")]
        public string Email { get; set; }

        /// <summary>
        /// Разряд стрелка
        /// </summary>
        [DisplayName("Разряд")]
        public int IdShooterCategory { get; set; }

        /// <summary>
        /// День рождения
        /// </summary>
        [DisplayName("Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd.MM.yyyy")]
        public DateTime DateBirthday { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [DisplayName("Пол")]
        public int Sex { get; set; }

        /// <summary>
        /// Ид. стрелкового клуба
        /// </summary>
        [DisplayName("Стрелковый клуб")]
        public int IdClub { get; set; }

        /// <summary>
        /// Тип оружия стрелка
        /// </summary>
        [DisplayName("Тип оружия")]
        public int IdWeaponType { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [DisplayName("Адрес")]
        public string Address { get; set; }
      
        /*public UserParams GetUser(int idUser)
        {
            return _userLogic.Get(idUser);
        }

        /// <summary>
        /// Получить список клубов по региону
        /// </summary>
        /// <param name="idCountry"></param>
        /// <param name="idRegion"></param>
        /// <returns></returns>
        public List<SelectListItem> GetShooterClubsByRegion(int idCountry, int idRegion)
        {
            return _shootingClubLogic.GetByRegion(idCountry, idRegion).ConvertAll(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name});
        }

        /// <summary>
        /// Получить список разрядов
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCategoies()
        {
            return _categoryLogic.GetAll().ConvertAll(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name});
        }

        private ShooterParams GetShooterByUser(int idUser)
        {
            return _shooterLogic.GetByUser(idUser);
        }*/

        /*/// <summary>
        /// Получить модель по существующему юзеру
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public static RegistrationPostModel GetModelByExistUser(int idUser)
        {
            var model = new RegistrationPostModel();

            //model.RegionsList = model.GetRegionsList();
            model.RolesList = StandartClassifierModelLogic.GetRolesList();
            model.SexList = StandartClassifierModelLogic.GetSexList();
            model.WeaponTypes = StandartClassifierModelLogic.GetWeaponTypeList();
            model.CountriesList = StandartClassifierModelLogic.GetCountryList();
            model.Categories = model.GetCategoies();

            model.IsEditMode = true;
            var user = model.GetUser(idUser);
            model.IdExistingUser = idUser;

            model.Family = user.FamilyName;
            model.Name = user.Name;
            model.FatherName = user.FatherName;
            model.Login = user.Login;
            model.Password = user.Password;
            model.IdRole = user.IdRole;
            model.Email = user.Email;

            if (model.IdRole == (int) RolesEnum.Shooter)
            {
                var shooter = model.GetShooterByUser(idUser);

                model.IdClub = shooter.IdClub;
                model.IdShooterCategory = shooter.IdCategory;
                model.IdWeaponType = shooter.IdWeaponType;
                model.Sex = shooter.Sex;
                model.DateBirthday = shooter.BirthDate;
                model.Address = shooter.Address;

                try
                {
                    var region = model._regionsLogic.GetRegionByClub(model.IdClub);
                    var country = model._countryLogic.GetCountryByRegion(region.Id).Data;
                    //model.CountriesList = model._countryLogic.GetAllCounties().Data.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.CountryName }).ToList(); ;
                    model.RegionsList = model._regionsLogic.GetByCountry(country.Id).Data.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name}).ToList();
                    model.RegionsList.Where(x => x.Value == region.Id.ToString()).Single().Selected = true;
                    model.CountriesList.Where(x => x.Value == country.Id.ToString()).Single().Selected = true;
                    model.ShooterClubs = model.GetShooterClubsByRegion(-1, region.Id); /// TODO: Здесь временный костыль с -1
                    model.ShooterClubs.Where(x => x.Value == model.IdClub.ToString()).Single().Selected = true;
                }
                catch (Exception exc)
                {
                    // что за долбанная жесть))
                    // Не удалось корректно показать стрелковый клуб и регион в модели, не беда )
                }
            }

            return model;
        }

        /// <summary>
        /// Добавляем пользователя 
        /// </summary>
        /// <param name="model"></param>
        public ResultInfoStruct<int> AddUser(RegistrationPostModel model)
        {
            var res = new ResultInfoStruct<int>();
            var user = new UserParams
            {
                DateCreate = DateTime.Now,
                FamilyName = model.Family,
                Name = model.Name,
                FatherName = model.FatherName,
                IdRole = model.IdRole,
                Login = model.Login,
                Password = model.Password,
                Email = model.Email
            };

            if (model.IdRole == (int) RolesEnum.Organization)
            {
                res = _userLogic.AddOrganizator(user);
            }
            else
            {
                if (model.IdRole == (int) RolesEnum.Shooter)
                {
                    var shooter = new ShooterParams
                    {
                        Address = model.Address,
                        BirthDate = model.DateBirthday,
                        DateCreate = DateTime.Now,
                        Family = model.Family,
                        Name = model.Name,
                        FatherName = model.FatherName,
                        Sex = model.Sex,
                        IdCategory = model.IdShooterCategory,
                        IdClub = model.IdClub,
                        IdUser = 0, // проинициализируется после добавления,
                        IdWeaponType = model.IdWeaponType
                    };

                    res = _userLogic.AddShooter(user, shooter);
                }
            }

            return res;
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <param name="model"></param>
        /// <param name="needUpdatePassword">требуется обновление пароля</param>
        /// <returns></returns>
        public ResultInfo UpdateUser(int idUser, RegistrationPostModel model, bool needUpdatePassword)
        {
            var res = new ResultInfo();

            var existingUser = _userLogic.Get(idUser);
            bool needUpateShooter = existingUser.IdRole == (int) RolesEnum.Shooter;

            ShooterParams shooter = null;
            if (needUpateShooter)
            {
                shooter = _shooterLogic.GetByUser(idUser);
                shooter.IdCategory = model.IdShooterCategory;
                shooter.IdClub = model.IdClub;
                shooter.IdWeaponType = model.IdWeaponType;
                shooter.IdUser = idUser;
                shooter.Name = model.Name;
                shooter.Family = model.Family;
                shooter.FatherName = model.FatherName;
            }

            var userParams = new UserParams
            {
                Id = idUser,
                IdRole = existingUser.IdRole,
                FamilyName = model.Family,
                FatherName = model.FatherName,
                Name = model.Name,
                Login = model.Login,
                Password = model.Password,
                Email = model.Email
            };

            res = _userLogic.Update(idUser, userParams, needUpdatePassword);
            if (res.IsOk)
            {
                if (needUpateShooter)
                {
                    res = _shooterLogic.Update(shooter.Id, shooter);
                }
            }

            return res;
        }*/
    }
}