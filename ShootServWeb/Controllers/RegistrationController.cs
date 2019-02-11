using System.Linq;
using System.Web.Mvc;
using BL;
using BO;
using ShootServ.Models;
using ShootServ.Models.Registration;

namespace ShootServ.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly RegistrationPostModel _modelLogic;
        private readonly ShooterCategoryLogic _categoryLogic;
        private readonly UserLogic _userLogic;
        private readonly ShooterLogic _shooterLogic;
        private readonly RegionsLogic _regionsLogic;
        private readonly CountryLogic _countryLogic;
        private readonly ShootingClubLogic _shootingClubLogic;

        public RegistrationController()
        {
            _modelLogic = new RegistrationPostModel();
            _categoryLogic = new ShooterCategoryLogic();
            _userLogic = new UserLogic();
            _shooterLogic = new ShooterLogic();
            _regionsLogic = new RegionsLogic();
            _countryLogic = new CountryLogic();
            _shootingClubLogic = new ShootingClubLogic();
        }

        private RegistrationPageModel GetPageModel()
        {
            return new RegistrationPageModel
            {
                RolesList = StandartClassifierModelLogic.GetRolesList(),
                SexList = StandartClassifierModelLogic.GetSexList(),
                WeaponTypes = StandartClassifierModelLogic.GetWeaponTypeList(),
                CountriesList = StandartClassifierModelLogic.GetCountryList(),
                Categories = _categoryLogic.GetAll().ConvertAll(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name})
            };
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

                var region = _regionsLogic.GetRegionByClub(postModel.IdClub);
                var country = _countryLogic.GetCountryByRegion(region.Id).Data;

                model.RegionsList = _regionsLogic.GetByCountry(country.Id).Data
                    .Select((x) => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == region.Id }).ToList();
                model.CountriesList.Single(x => x.Value == country.Id.ToString()).Selected = true;
                
                model.ShooterClubs = _shootingClubLogic.GetByRegion(country.Id, region.Id)
                    .Select((x) => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == postModel.IdClub }).ToList();
            }

            return model;
        }

        public ActionResult Index(int? idUser)
        {
            RegistrationPageModel model = null;

            if (idUser.HasValue)
            {
                model = GetPageModel();
            }
            else
            {
                if (CurrentUser != null)
                {
                    model = GetModelByExistUser(idUser.Value);
                }
                else
                {
                    return Redirect(Url.Action("Index", "Home", new {Area = ""}));
                }
            }

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
            
            if (model.IdRole == (int)RolesEnum.Organization)
            {
                addResult = _userLogic.AddOrganizator(userParams);
            }

            if (model.IdRole == (int) RolesEnum.Shooter)
            {
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

        [HttpGet]
        public ActionResult UpdateUser(int idExistingUser, bool needUpdatePassword, RegistrationPostModel model)
        {
            var res = new ResultInfo {IsOk = false};
            if (((UserParams) Session["user"]).Id == idExistingUser)
            {
                res = _modelLogic.UpdateUser(idExistingUser, model, needUpdatePassword);
            }

            if (res.IsOk)
            {
                var updatingUser = _modelLogic.GetUser(idExistingUser);
                Session["user"] = updatingUser;
            }

            return new JsonResult {Data = new {IsOk = res.IsOk, Message = res.ErrorMessage}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}