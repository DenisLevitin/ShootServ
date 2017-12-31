using BL;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebGrease.Css.Extensions;
using ShootingCompetitionsRequests.Models;

namespace ShootingCompetitionsRequests.Areas.Cup.Models
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

        /// <summary>
        /// Осуществил ли пользователь вход на страницу
        /// </summary>
        public bool IsLogin { get; set; }

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
            IsLogin = false;
        }
    }

    /// <summary>
    /// Модель для упражнения
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CompetitionModelParams 
    {
        /// <summary>
        /// ид. типа упражнения
        /// </summary>
        [JsonProperty]
        public int IdCompetitionType { get; set; }

        /// <summary>
        /// Ид. упражнения на соревновании
        /// </summary>
        [JsonProperty]
        public int IdCupCompetitionType { get; set; }

        /// <summary>
        /// Ид. типа оружия
        /// </summary>
        public int IdWeaponType { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// Время начала первой смены
        /// </summary>
        [JsonProperty]
        public DateTime TimeFirstShift { get; set; }

        /// <summary>
        /// Заявлено в соревновании
        /// </summary>
        public bool IsInCup { get; set; }

        public CompetitionModelParams()
        {
            IsInCup = false;
            IdCupCompetitionType = 0;
        }
    }

    /// <summary>
    /// Логика модели по работе с кубком
    /// </summary>
    public class CupModelLogic
    {
        private readonly CupLogic _cupLogic;
        private readonly CupTypeLogic _cupTypeLogic;
        private readonly RegionsLogic _regionLogic;
        private readonly ShootingRangeLogic _shootingRangeLogic;
        private readonly CompetitionTypeLogic _competitionTypeLogic;
        private readonly CupCompetitionTypeLogic _cupCompetitionTypeLogic;
        private readonly UserLogic _userLogic;

        public CupModelLogic()
        {
            _cupLogic = new CupLogic();
            _cupTypeLogic = new CupTypeLogic();
            _regionLogic = new RegionsLogic();
            _shootingRangeLogic = new ShootingRangeLogic();
            _competitionTypeLogic = new CompetitionTypeLogic();
            _userLogic = new UserLogic();
            _cupCompetitionTypeLogic = new CupCompetitionTypeLogic();
        }

        /// <summary>
        /// Получить список типов соревнований
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCupTypes()
        {
            var res = new List<SelectListItem>();

            var query = _cupTypeLogic.GetAll();
            foreach (var item in query)
            {
                res.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });
            }

            return res;
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

        //public ResultInfo LoadDocument(int idCup, Http)
        //{ }

        /// TODO: Добавить загрузку основания

        public List<CupDetailsParams> GetListCupsByRegionAndDates(int idRegion = -1, DateTime dateFrom = default(DateTime), DateTime dateTo = default(DateTime))
        {
            return _cupLogic.GetDetailsByRegionAndDates(idRegion, dateFrom, dateTo);
        }

        /// <summary>
        /// Получить список упражнений
        /// </summary>
        /// <returns></returns>
        public List<CompetitionModelParams> GetCompetitionsList()
        {
            var query = _competitionTypeLogic.GetAll();

            return query.Select(item => new CompetitionModelParams
            {
                IdCompetitionType = item.Id, Name = item.Name, TimeFirstShift = DateTime.Now
            }).ToList();
        }

        /// <summary>
        /// Добавить соревнование
        /// </summary>
        /// <param name="cup">соревнование</param>
        /// <param name="competitions">список упражнений</param>
        /// <param name="idUser">ид. пользователя</param>
        public ResultInfoStruct<int> AddCup(CupModelParams cup, List<CompetitionModelParams> competitions, int idUser)
        {
            var cupParams = new CupParams
            {
                Id = cup.Id,
                IdCupType = cup.IdCupType,
                IdShootingRange = cup.IdShootingRange,
                DateCreate = DateTime.Now,
                DateStart = cup.DateStart,
                DateEnd = cup.DateEnd,
                IdUser = idUser,
                Name = cup.Name
            };

            var compTypes = competitions.Select(item => new CupCompetitionTypeParams
            {
                IdCompetitionType = item.IdCompetitionType, TimeFirstShift = item.TimeFirstShift
            }).ToList();

            return _cupLogic.AddCupWithCompetitions(cupParams, compTypes);
        }

        /// <summary>
        /// Получить модель для индекс страницы
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public CupModelParams GetModelForIndex(int idCup)
        {
            var model = new CupModelParams();

            model.Countries = StandartClassifierModelLogic.GetCountryList().Data; // немного говнокода
            model.CupTypes = GetCupTypes();
            model.CompetitionTypes = GetCompetitionsList();
            model.IsEditMode = false;

            if (idCup != -1) // Если надо получить модель для соревнования, которое уже существует
            {
                model.IsEditMode = true; // Модель в режиме редактирования
                
                var cup = _cupLogic.Get(idCup); // Загружаем существующее соревнование

                model.Name = cup.Name;
                model.DateCreate = cup.DateCreate;
                model.DateStart = cup.DateStart;
                model.DateEnd = cup.DateEnd;
                model.Document = cup.Document;
                model.Id = cup.Id;
                model.IdCupType = cup.IdCupType;

                var region = _regionLogic.GetRegionByShootingRange(cup.IdShootingRange);
                if (region != null)
                {
                    model.IdRegion = region.Id;
                }

                model.ShootingRanges = this.GetShootingRangesByRegion(model.IdRegion);

                model.IdShootingRange = cup.IdShootingRange;
                model.IdUser = cup.IdUser;

                try
                {
                    model.CupTypes.Where(x => Convert.ToInt32(x.Value) == model.IdCupType)
                        .Select(y => y.Selected)
                        .ToList()
                        .ForEach(x => x = true);
                    model.ShootingRanges.Where(x => Convert.ToInt32(x.Value) == model.IdShootingRange)
                        .Select(y => y.Selected)
                        .ToList()
                        .ForEach(x => x = true);
                    model.Regions.Where(x => Convert.ToInt32(x.Value) == model.IdRegion)
                        .Select(y => y.Selected)
                        .ToList()
                        .ForEach(x => x = true);
                }
                catch (Exception exc)
                {
                }

                // Отмечаем упражнения, которые есть на соревновании
                var competitions = _cupCompetitionTypeLogic.GetByCup(idCup);

                foreach (var item in model.CompetitionTypes)
                {
                    var cupCompType = competitions.Where(x => x.IdCompetitionType == item.IdCompetitionType);
                    if (cupCompType.Any())
                    {
                        var cupCompTypeFirst = cupCompType.First();
                        item.IsInCup = true;
                        item.IdCupCompetitionType = cupCompTypeFirst.Id;
                        item.TimeFirstShift = cupCompTypeFirst.TimeFirstShift ?? default(DateTime);
                    }
                    
                } 

            }

            return model;
        }

        /// <summary>
        /// Получить соревнование
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public CupDetailsParams GetCup(int idCup)
        {
            return _cupLogic.GetDetailsCup(idCup);
        }

        /// <summary>
        /// Обновить соревнование
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idUser">ид. пользователя, выполняющего обновление</param>
        /// <param name="cupModel">соревнование</param>
        /// <param name="competitions">список упражнений</param>
        /// <returns></returns>
        public ResultInfo Update(int idCup, int idUser, CupModelParams cupModel, List<CompetitionModelParams> competitions )
        {
            var cup = new CupParams
            {
                Id = cupModel.Id,
                DateStart = cupModel.DateStart,
                DateEnd = cupModel.DateEnd,
                IdCupType = cupModel.IdCupType,
                IdShootingRange = cupModel.IdShootingRange,
                Name = cupModel.Name
            };

            var compTypes = competitions.Select(item => new CupCompetitionTypeParams
            {
                IdCompetitionType = item.IdCompetitionType,
                Id = item.IdCupCompetitionType,
                TimeFirstShift = item.TimeFirstShift,
                IdCup = idCup
            }).ToList();

            return _cupLogic.Update(idCup, idUser, cup, compTypes);
        }

        /// <summary>
        /// Удалить соревнование
        /// </summary>
        /// <param name="idCup"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public ResultInfo DeleteCup(int idCup, int idUser)
        {
            return _cupLogic.Delete(idCup, idUser);
        }

        /// <summary>
        /// Является ли пользователь стрелком
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <returns></returns>
        public bool IsUserShooter(UserParams user)
        {
            return user.IdRole == (int) RoleParams.RoleEnum.Shooter;
        }
    }
}