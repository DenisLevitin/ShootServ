using System.Data;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблицей Cups
    /// </summary>
    public class CupRepository
    {
        private Cups Convert(CupParams cup)
        {
            return new Cups
            {
                IdCup = cup.Id,
                IdCupType = cup.IdCupType,
                IdShootingRange = cup.IdShootingRange,
                IdUser = cup.IdUser,
                DateEnd = cup.DateEnd,
                DateStart = cup.DateStart,
                Document = cup.Document,
                Name = cup.Name,
                DateCreate = cup.DateCreate
            };
        }

        private CupParams Convert(Cups cup)
        {
            return new CupParams
            {
                Id = cup.IdCup,
                IdCupType = cup.IdCupType,
                IdShootingRange = cup.IdShootingRange,
                IdUser = cup.IdUser,
                DateEnd = cup.DateEnd,
                DateStart = cup.DateStart,
                Document = cup.Document,
                Name = cup.Name,
                DateCreate = cup.DateCreate
            };
        }

        /// <summary>
        /// Добавить соревнование
        /// </summary>
        /// <param name="cup"></param>
        /// <returns></returns>
        public ResultInfoStruct<int> Add(CupParams cup)
        {
            var res = new ResultInfoStruct<int>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var adding = Convert(cup);
                    db.Cups.Add(adding);
                    db.SaveChanges();

                    res.Data = adding.IdCup;
                }
                catch (Exception exc)
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "Не удалось добавить соревнование в базу";
                    res.Result.Exc = exc;
                }
            }

            return res;
        }

        /// <summary>
        /// Обновить соревнование
        /// </summary>
        /// <param name="idCup">ид соревнования</param>
        /// <param name="cup">соревнование</param>
        /// <returns></returns>
        public ResultInfo Update(int idCup, CupParams cup)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var updating = new Cups { IdCup = idCup};
                    db.Cups.Attach(updating);

                    updating.IdCupType = cup.IdCupType;
                    updating.IdShootingRange = cup.IdShootingRange;
                    updating.IdUser = cup.IdUser;
                    updating.Name = cup.Name;
                    updating.DateCreate = cup.DateCreate;
                    updating.DateEnd = cup.DateEnd;
                    updating.DateStart = cup.DateStart;
                    updating.Document = cup.Document;

                    db.Entry(updating).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "При обновлении соревнования произошла ошибка";
                res.Exc = exc;
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
            CupParams res;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.Cups.Single(x => x.IdCup == id);
                    res = Convert(query);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении соревнования произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить по ид.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CupParams> GetAll()
        {
            List<CupParams> res;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    res = db.Cups.ToList().ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка соревнования произошла ошибка");
                }
            }

            return res;
        }

        
        /// <summary>
        /// Получить список соревнований по региону и датам
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <param name="dateFrom">дата с</param>
        /// <param name="dateTo">дата по</param>
        /// <returns></returns>
        public List<CupParams> GetByRegionAndDates(int idRegion, DateTime dateFrom, DateTime dateTo)
        {
            List<CupParams> res;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from cup in db.Cups
                           join shootingRange in db.ShootingRanges on cup.IdShootingRange equals shootingRange.Id
                           where shootingRange.IdRegion == idRegion && cup.DateStart >= dateFrom && cup.DateStart <= dateTo
                           select cup).ToList();

                    res = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка соревнования произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить список соревнований по региону и датам
        /// </summary>
        /// <param name="idShootingRange">ид. тира</param>
        /// <param name="dateFrom">дата с</param>
        /// <param name="dateTo">дата по</param>
        /// <returns></returns>
        public List<CupParams> GetByShootingRangeAndDates(int idShootingRange, DateTime dateFrom, DateTime dateTo)
        {
            List<CupParams> res;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from cup in db.Cups
                                 where cup.DateStart >= dateFrom && cup.DateStart <= dateTo && cup.IdShootingRange == idShootingRange
                                 select cup).ToList();

                    res = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка соревнований произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Удалить соревнование
        /// </summary>
        /// <param name="idCup"></param>
        /// <returns></returns>
        public ResultInfo Delete(int idCup)
        {
            var res = new ResultInfo();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var delete = db.Cups.Single(x => x.IdCup == idCup);
                    db.Cups.Remove(delete);
                    db.SaveChanges();
                }
                catch (Exception exc)
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Не удалось удалить соревнование из базы";
                    res.Exc = exc;
                }
            }

            return res;
        }

        /// <summary>
        /// Получить запрос соревнований по датам
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        private IQueryable<Cups> GetQueryCupsByDates(ShootingCompetitionRequestsEntities db, DateTime dateFrom, DateTime dateTo)
        {
            return dateFrom != default(DateTime) && dateTo != default(DateTime) ? db.Cups.Where(x => x.DateStart >= dateFrom && x.DateStart <= dateTo) : db.Cups;
        }

        /// <summary>
        /// Получить список соревнований с детализацией
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<CupDetailsParams> GetDetailsByRegionAndDates(int idRegion = -1, DateTime dateFrom = default(DateTime), DateTime dateTo = default(DateTime))
        {
            var res = new List<CupDetailsParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var cups = this.GetQueryCupsByDates(db, dateFrom, dateTo); // запрос по соревнованиям
                    var shootingRanges = new ShootingRangeRepository().GetQueryShootingRangesByRegion(db, idRegion); // запрс по тирам

                    var query = (from cup in cups
                                 join shootingRange in shootingRanges on cup.IdShootingRange equals shootingRange.Id
                                 join cupType in db.CupTypes on cup.IdCupType equals cupType.Id
                                 join region in db.Regions on shootingRange.IdRegion equals region.IdRegion
                                 select new
                                 {
                                     Cup = cup,
                                     ShootingRangeName = shootingRange.Name,
                                     CupType = cupType.Name,
                                     Region = region.Name,
                                     Town = shootingRange.Town,
                                     ShootingRangeAddress = shootingRange.Address,
                                     ShootingRangePhone = shootingRange.Telefon
                                 }).ToList();

                    res.AddRange(query.Select(item => new CupDetailsParams
                    {
                        Id = item.Cup.IdCup,
                        Name = item.Cup.Name,
                        DateStart = item.Cup.DateStart,
                        DateEnd = item.Cup.DateEnd,
                        CupType = item.CupType,
                        RangeName = item.ShootingRangeName,
                        Town = item.Town,
                        Region = item.Region,
                        RangeAddress = item.ShootingRangeAddress,
                        RangePhone = item.ShootingRangePhone
                    }));
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Не удалось получить список соревнований");
            }

            return res;
        }

        /// <summary>
        /// Получить детализацию соревнования
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public CupDetailsParams GetDetailsCup(int idCup)
        {
            var res = new CupDetailsParams();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = (from cup in db.Cups
                                 join shootingRange in db.ShootingRanges on cup.IdShootingRange equals shootingRange.Id
                                 join cupType in db.CupTypes on cup.IdCupType equals cupType.Id
                                 join region in db.Regions on shootingRange.IdRegion equals region.IdRegion
                                 where cup.IdCup == idCup
                                 select new
                                 {
                                     Cup = cup,
                                     ShootingRangeName = shootingRange.Name,
                                     CupType = cupType.Name,
                                     Region = region.Name,
                                     Town = shootingRange.Town,
                                     ShootingRangeAddress = shootingRange.Address,
                                     ShootingRangePhone = shootingRange.Telefon
                                 }).Single();

                        res = new CupDetailsParams
                        {
                            Id = query.Cup.IdCup,
                            Name = query.Cup.Name,
                            DateStart = query.Cup.DateStart,
                            DateEnd = query.Cup.DateEnd,
                            CupType = query.CupType,
                            RangeName = query.ShootingRangeName,
                            Town = query.Town,
                            Region = query.Region,
                            RangeAddress = query.ShootingRangeAddress,
                            RangePhone = query.ShootingRangePhone
                        };

                }
            }
            catch (Exception exc)
            {
                throw new Exception("Не удалось получить соревнование");
            }

            return res;
        }

    }
}
