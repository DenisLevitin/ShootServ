using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BL;
using BO;
using ShootingCompetitionsRequests.Models;

namespace ShootServ.Models.Cup
{
    /// <summary>
    /// Логика модели по работе с кубком
    /// </summary>
    public class CupModelLogic
    {
        private readonly CupLogic _cupLogic;
        private readonly RegionsLogic _regionLogic;
        private readonly ShootingRangeLogic _shootingRangeLogic;
        private readonly CompetitionTypeLogic _competitionTypeLogic;
        private readonly CupCompetitionTypeLogic _cupCompetitionTypeLogic;

        public CupModelLogic()
        {
            _cupLogic = new CupLogic();
            _regionLogic = new RegionsLogic();
            _shootingRangeLogic = new ShootingRangeLogic();
            _competitionTypeLogic = new CompetitionTypeLogic();
            _cupCompetitionTypeLogic = new CupCompetitionTypeLogic();
        }

        /// <summary>
        /// Получить список типов соревнований
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetCupTypes()
        {
            var enumValues = EnumHelper.GetEnumValues<CupTypeParams>();
            return enumValues.Select(item => new SelectListItem {Value = item.Id.ToString(), Text = item.EnumDescription}).ToList();
        }

        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public List<SelectListItem> GetShootingRangesByRegion(int? idRegion)
        {
            return _shootingRangeLogic.GetByRegion(idRegion).ConvertAll(x => new SelectListItem { Text = string.Format("{0} {1}", x.Town, x.Name), Value = x.Id.ToString() });
        }

        /// TODO: Добавить загрузку основания

        public List<CupDetailsParams> GetCupsByRegionAndDates(int? idRegion, DateTime? dateFrom, DateTime? dateTo)
        {
            return _cupLogic.GetCupsDetailsByRegionAndDates(idRegion, dateFrom, dateTo);
        }

        /// <summary>
        /// Получить список упражнений
        /// </summary>
        /// <returns></returns>
        private List<CompetitionModelParams> GetCompetitionsList()
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
            var model = new CupModelParams
            {
                Countries = StandartClassifierModelLogic.GetCountryList().Data, 
                CupTypes = GetCupTypes(),
                CompetitionTypes = GetCompetitionsList(), 
                IsEditMode = false
            };

            // немного говнокода
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

                model.ShootingRanges = GetShootingRangesByRegion(model.IdRegion);

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
                    // logger 
                }

                // Отмечаем упражнения, которые есть на соревновании
                var competitions = _cupCompetitionTypeLogic.GetByCup(idCup);

                foreach (var item in model.CompetitionTypes)
                {
                    var cupCompType = competitions.Where(x => x.IdCompetitionType == item.IdCompetitionType).ToList();
                    if (cupCompType.Count > 0)
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
        /// Является ли пользователь стрелком
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns></returns>
        public static bool IsUserShooter(UserParams user)
        {
            return user.IdRole == (int) RoleParams.RoleEnum.Shooter;
        }
    }
}