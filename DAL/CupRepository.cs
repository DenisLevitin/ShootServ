using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблицей Cups
    /// </summary>
    public class CupRepository : BaseRepository<CupParams, Cups>
    {
        protected override Cups ConvertToEntity(CupParams cup)
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

        protected override Func<Cups, int> GetPrimaryKeyValue
        {
            get { return (x) => x.IdCup; }
        }

        protected override CupParams ConvertToModel(Cups cup)
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
        /// Получить список соревнований по региону и датам
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <param name="dateFrom">дата с</param>
        /// <param name="dateTo">дата по</param>
        /// <returns></returns>
        public List<CupParams> GetByRegionAndDates(int idRegion, DateTime dateFrom, DateTime dateTo)
        {
            using (var db = DBContext.GetContext())
            {
                var query = (from cup in db.Cups
                    join shootingRange in db.ShootingRanges on cup.IdShootingRange equals shootingRange.Id
                    where shootingRange.IdRegion == idRegion && cup.DateStart >= dateFrom && cup.DateStart <= dateTo
                    select cup).ToList();

                return query.ConvertAll(ConvertToModel);
            }
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
            using (var db = DBContext.GetContext())
            {
                var query = (from cup in db.Cups
                    where cup.DateStart >= dateFrom && cup.DateStart <= dateTo && cup.IdShootingRange == idShootingRange
                    select cup).ToList();

                return query.ConvertAll(ConvertToModel);
            }
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
        public List<CupDetailsParams> GetCupsDetailsByRegionAndDates(int idRegion = -1, DateTime dateFrom = default(DateTime), DateTime dateTo = default(DateTime))
        {
            using (var db = DBContext.GetContext())
            {
                var cups = GetQueryCupsByDates(db, dateFrom, dateTo); // запрос по соревнованиям
                var shootingRanges = idRegion > 0 ? db.ShootingRanges.Where(x => x.IdRegion == idRegion) : db.ShootingRanges;

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

                return query.Select(item => new CupDetailsParams
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
                }).ToList();
            }
        }

        /// <summary>
        /// Получить детализацию соревнования
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public CupDetailsParams GetDetailsCup(int idCup)
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

                return new CupDetailsParams
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
    }
}