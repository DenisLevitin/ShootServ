using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BL
{
    /// <summary>
    /// БЛ соревнований
    /// </summary>
    public class CupLogic
    {
        private readonly CupRepository _dalCup;
        private readonly UserLogic _userLogic;
        private readonly CupCompetitionTypeRepository _dalCupCompType;
        private readonly EntryForCompetitionsRepository _dalEntryComp;

        public CupLogic()
        {
            _dalCup = new CupRepository();
            _userLogic = new UserLogic();
            _dalCupCompType = new CupCompetitionTypeRepository();
            _dalEntryComp = new EntryForCompetitionsRepository();
        }

        /// <summary>
        /// Добавить соревнование
        /// </summary>
        /// <param name="cup"></param>
        /// <returns></returns>
        public ResultInfoStruct<int> Add(CupParams cup)
        {
            var res = new ResultInfoStruct<int>();

            var queryUser = _userLogic.Get(cup.IdUser);
            if (queryUser.IdRole == (int) RolesEnum.Organization)
            {
                res.Data = _dalCup.Create(cup);
            }
            else
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Пользователь не является организатором, поэтому не может создать соревнование";
            }

            return res;
        }

        /// <summary>
        /// Добавить соревнования с упражнениями
        /// </summary>
        /// <param name="cup">соревнование</param>
        /// <param name="compTypes">упражнения</param>
        /// <returns></returns>
        public ResultInfoStruct<int> AddCupWithCompetitions(CupParams cup, List<CupCompetitionTypeParams> compTypes)
        {
            var res = new ResultInfoStruct<int>();

            using (var tran = new TransactionScope())
            {
                var queryAddCup = this.Add(cup);
                if (queryAddCup.Result.IsOk)
                {
                    res.Data = queryAddCup.Data;

                    foreach (var item in compTypes)
                    {
                        item.IdCup = queryAddCup.Data;
                        res.Data = _dalCupCompType.Create(item);

                        if (!res.Result.IsOk)
                        {
                            break;
                        }
                    }

                    if (res.Result.IsOk)
                    {
                        tran.Complete();
                    }
                }
                else res.Result = queryAddCup.Result;
            }

            return res;
        }

        /// <summary>
        /// Получить по ид.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CupParams Get(int id)
        {
            return _dalCup.Get(id);
        }

        /// <summary>
        /// Получить список соревнований с детализацией
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<CupDetailsParams> GetCupsDetailsByRegionAndDates(int? idRegion, DateTime? dateFrom, DateTime? dateTo)
        {
            return _dalCup.GetCupsDetailsByRegionAndDates(idRegion, dateFrom, dateTo);
        }

        /// <summary>
        /// Получить список соревнований по тиру и периоду
        /// </summary>
        /// <param name="idShootingRange"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<CupParams> GetByShootingRangeAndDates(int idShootingRange, DateTime dateFrom, DateTime dateTo)
        {
            return _dalCup.GetByShootingRangeAndDates(idShootingRange, dateFrom, dateTo);
        }

        /// <summary>
        /// Получить список всех соревнований
        /// </summary>
        /// <returns></returns>
        public List<CupParams> GetAll()
        {
            return _dalCup.GetAll();
        }

        /// <summary>
        /// Получить детализацию соревнования
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public CupDetailsParams GetDetailsCup(int idCup)
        {
            return _dalCup.GetDetailsCup(idCup);
        }

        /// <summary>
        /// Обновить соревнование
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idUser">ид. пользователя, выполняющего обновление</param>
        /// <param name="cup">соревнование</param>
        /// <param name="competitions">список упражнений на обновление</param>
        /// <returns></returns>
        public ResultInfo Update(int idCup, int idUser, CupParams cup, List<CupCompetitionTypeParams> competitions)
        {
            var res = new ResultInfo();

            var user = _userLogic.Get(idUser);
            if (user.Id == idUser) // Проверка на пользователя
            {
                var oldCup = _dalCup.Get(idCup);

                cup.DateCreate = oldCup.DateCreate;
                cup.IdUser = oldCup.IdUser;

                // Проверка на удаляемые упражнения, есть ли на них заявки
                var competitionsExists = _dalCupCompType.GetByCup(idCup); // упражнения, которые включены в соревнования
                var deleting = competitionsExists.Select(x => x.IdCompetitionType).Except(competitions.Select(x => x.IdCompetitionType)); // то, что следует удалить

                var existCompetitionsWithEntry = _dalCupCompType.GetByCupWithEntry(idCup); // существующие заявки
                bool isExistsEntryForDeleting = deleting.Any(x => existCompetitionsWithEntry.Select(y => y.IdCompetitionType).Contains(x)); // определяем существуют ли заявки на упражнения, которые удаляются

                if (!isExistsEntryForDeleting)
                {
                    // Определяем список добавляемых упражнений
                    var inserting = competitions.Select(x => x.IdCompetitionType).Except(competitionsExists.Select(y => y.IdCompetitionType));
                    var listInserting = competitions.Where(x => inserting.Contains(x.IdCompetitionType)).ToList();
                    listInserting.ForEach(y => y.IdCup = idCup);

                    using (var tran = new TransactionScope())
                    {
                        // 1. Добавляем упражнения, на соревнования
                        _dalCupCompType.AddRange(listInserting);

                        var listDeleting = competitionsExists.Where(x => deleting.Contains(x.IdCompetitionType)).ToList();

                        // 2. Удаляем упражнения с соревнование
                        res = _dalCupCompType.DelRange(listDeleting);
                        if (res.IsOk)
                        {
                            _dalCup.Update(cup, idCup);
                            var intersectingIds = new HashSet<int>(competitions.Select(x => x.IdCompetitionType).Intersect(competitionsExists.Select(y => y.IdCompetitionType))); // Определяем множество упражнений, которые остались
                            var intersecting = competitions.Where(x => intersectingIds.Contains(x.IdCompetitionType));

                            // 3. У упражнений, которые не надо удалить или добавить обновить все атрибуты ( пока что время начала первой смены )
                            foreach (var item in intersecting)
                            {
                                _dalCupCompType.Update(item, item.Id); // Косяк в том, что не понятно как проинициализировать IdCupCompetitionType
                            }

                            if (res.IsOk)
                            {
                                tran.Complete();
                            }
                        }
                    }
                }
                else
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Невозможно отредактировать соревнование, т.к вы удаляете упражнения, на которые уже есть заявки";
                }
            }
            else
            {
                res.IsOk = false;
                res.ErrorMessage = "Пользователь не может обновить соревнование, т.к. не он его создавал";
            }

            return res;
        }

        /// <summary>
        /// Удалить соревнование
        /// </summary>
        /// <param name="idCup">ид соревнования</param>
        /// <param name="idUser">ид пользователя</param>
        /// <returns></returns>
        public ResultInfo Delete(int idCup, int idUser)
        {
            var res = new ResultInfo();

            // Проверяем наличие заявок
            var entries = _dalEntryComp.GetByCup(idCup);
            if (entries.Count == 0)
            {
                var cup = _dalCup.Get(idCup);
                // Можем удалить соревнование, только если пользователь его создал
                if (cup.IdUser == idUser)
                {
                    /// ОБЯЗАТЕЛЬНО ДОЛЖНО БЫТЬ КАСКАДНОЕ УДАЛЕНИЕ УПРАЖНЕНИЙ НА СОРЕВНОВАНИИ
                    _dalCup.Delete(idCup);
                }
                else
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Пользователь не может удалить соревнование, т.к не он его создал";
                }
            }
            else
            {
                res.IsOk = false;
                res.ErrorMessage = "Нельзя удалить соревнование, т.к на него уже поданы заявки";
            }

            return res;
        }
    }
}