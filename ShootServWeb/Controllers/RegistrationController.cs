using System;
using System.Linq;
using System.Web.Mvc;
using BL;
using BO;
using Serilog;
using ShootServ.Models;
using ShootServ.Models.Registration;

namespace ShootServ.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly ShooterCategoryLogic _categoryLogic;
        private readonly UserLogic _userLogic;
        private readonly ShooterLogic _shooterLogic;
        private readonly RegionsLogic _regionsLogic;
        private readonly CountryLogic _countryLogic;
        private readonly ShootingClubLogic _shootingClubLogic;

        public RegistrationController(ILogger logger) : base(logger)
        {
            _categoryLogic = new ShooterCategoryLogic();
            _userLogic = new UserLogic();
            _shooterLogic = new ShooterLogic();
            _regionsLogic = new RegionsLogic();
            _countryLogic = new CountryLogic();
            _shootingClubLogic = new ShootingClubLogic();
        }

        private RegistrationPageModel GetPageModel()
        {
            var result = new RegistrationPageModel
            {
                RolesList = StandartClassifierModelLogic.GetRolesList(),
                SexList = StandartClassifierModelLogic.GetSexList(),
                WeaponTypes = StandartClassifierModelLogic.GetWeaponTypeList(),
                CountriesList = StandartClassifierModelLogic.GetCountryList(),
                Categories = _categoryLogic.GetAll().ConvertAll(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name}),
            };
            
            result.PostModel.DateBirthday = DateTime.Now.AddYears(-18);
            return result;
        }

        /// <summary>
        /// Получить модель по существующему юзеру
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [NonAction]
        private RegistrationPageModel GetModelByExistUser(int idUser)
        {
            var model = GetPageModel();
            model.IsEditMode = true;

            var user = _userLogic.Get(idUser);

            var postModel = model.PostModel;
            postModel.IdExistingUser = idUser;
            postModel.Family = user.FamilyName;
            postModel.Name = user.Name;
            postModel.FatherName = user.FatherName;
            postModel.Login = user.Login;
            postModel.Password = user.Password;
            postModel.IdRole = user.IdRole;
            postModel.Email = user.Email;

            if (postModel.IdRole == (int) RolesEnum.Shooter)
            {
                var shooter = _shooterLogic.GetByUser(idUser);

                postModel.IdClub = shooter.IdClub;
                postModel.IdShooterCategory = shooter.IdCategory;
                postModel.IdWeaponType = shooter.IdWeaponType;
                postModel.Sex = shooter.Sex;
                postModel.DateBirthday = shooter.BirthDate;
                postModel.Address = shooter.Address;

                var region = postModel.IdClub.HasValue ? _regionsLogic.GetRegionByClub(postModel.IdClub.Value) : null;
                var country = region != null ? _countryLogic.GetCountryByRegion(region.Id).Data : null;

                model.RegionsList = _regionsLogic.GetByCountry(country?.Id)
                    .Select((x) => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == region?.Id }).ToList();
                
                model.CountriesList.ForEach((x) =>
                {
                    if (x.Value == country?.Id.ToString())
                    {
                        x.Selected = true;
                    }
                });
                
                model.ShooterClubs = _shootingClubLogic.GetByRegion(country?.Id, region?.Id)
                    .Select((x) => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == postModel.IdClub }).ToList();
            }

            return model;
        }

        public ActionResult Index()
        {
            var model = CurrentUser != null ? GetModelByExistUser(CurrentUser.Id) : GetPageModel();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult AddUser(RegistrationPostModel model)
        {
            ResultInfoStruct<int> addResult = null;
            var userParams = new UserParams()
            {
                Name = model.Name,
                FamilyName = model.Family,
                Login = model.Login,
                Password = model.Password,
                IdRole = model.IdRole,
                Email = model.Email
            };
            
            switch (model.IdRole)
            {
                case (int)RolesEnum.Organization:
                    addResult = _userLogic.AddOrganizator(userParams);
                    break;
                case (int) RolesEnum.Shooter:
                    addResult = _userLogic.AddShooter(userParams, new ShooterParams()
                    {
                        Family = model.Family,
                        Name = model.Name,
                        FatherName = model.FatherName,
                        IdClub = model.IdClub,
                        Address = model.Address,
                        BirthDate = model.DateBirthday,
                        IdCategory = model.IdShooterCategory,
                        IdWeaponType = model.IdWeaponType,
                        Sex = model.Sex
                    });
                   break;
                default: throw new Exception("Неизвестная роль пользователя");
                    break;
            }

            if (addResult.Result.IsOk)
            {
                // Регистрация прошла успешно, сразу пишем юзера в сессию
                var user = _userLogic.Get(addResult.Data);
                if (user != null)
                {
                    Session["user"] = user;
                }
            }

            return new JsonResult { Data = new { IsOk = addResult.Result.IsOk, Message = addResult.Result.ErrorMessage }};
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult UpdateUser(RegistrationPostModel model)
        {
            if (IsLogin)
            {
                var userParams = new UserParams()
                {
                    IdRole = model.IdRole,
                    Email = model.Email,
                    Login = model.Login,
                    FamilyName = model.Family,
                    Password = model.Password
                };
                
                _userLogic.Update(CurrentUser.Id, userParams, false);
                Session["user"] = _userLogic.Get(CurrentUser.Id);
            }

            return new JsonResult {Data = new {IsOk = true, Message = string.Empty}};
        }
    }
}