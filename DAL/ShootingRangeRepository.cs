using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Класс для работы с таблицей ShootingRange
    /// </summary>
    public class ShootingRangeRepository
    {
        private ShootingRangeParams Convert(ShootingRanges dalShootingRange)
        {
            return new ShootingRangeParams
            {
                Address = dalShootingRange.Address,
                Id = dalShootingRange.Id,
                IdRegion = dalShootingRange.IdRegion,
                Info = dalShootingRange.Info,
                Name = dalShootingRange.Name,
                Phone = dalShootingRange.Telefon,
                Town = dalShootingRange.Town,
                IdUser = dalShootingRange.IdUser
            };
        }

        private ShootingRanges Convert(ShootingRangeParams shootingRange)
        {
            return new ShootingRanges
            {
                Id = shootingRange.Id,
                Address = shootingRange.Address,
                IdRegion = shootingRange.IdRegion,
                Info = shootingRange.Info ?? "",
                Name = shootingRange.Name,
                Telefon = shootingRange.Phone ?? "",
                Town = shootingRange.Town ?? "",
                IdUser = shootingRange.IdUser
            };
        }

        public IQueryable<ShootingRanges> GetQueryShootingRangesByRegion(ShootingCompetitionRequestsEntities db, int idRegion)
        {
            return idRegion > 0 ? db.ShootingRanges.Where(x => x.IdRegion == idRegion) : db.ShootingRanges;
        }

        /// <summary>
        /// Получить тир по идентификатору
        /// </summary>
        /// <param name="idShootingRange">ид. тира</param>
        /// <returns></returns>
        public ShootingRangeParams Get(int idShootingRange)
        {
            var res = new ShootingRangeParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    res = Convert(db.ShootingRanges.First(x => x.Id == idShootingRange));
                }
                catch (Exception exc)
                {
                    throw new Exception("Не найден стрелок по идентификатору");
                }
            }

            return res;
        }

        /// <summary>
        /// Удаляем тир
        /// </summary>
        /// <param name="idShootRange">ид. тира</param>
        /// <returns></returns>
        public ResultInfo Delete(int idShootRange)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var deleting = db.ShootingRanges.Single(x => x.Id == idShootRange);
                    db.ShootingRanges.Remove(deleting);

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "Произошла ошибка при удалении тира";
                res.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Добавить тир
        /// </summary>
        /// <param name="shootingRange">тир</param>
        public bool Add(ShootingRangeParams shootingRange)
        {
            bool res = true;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    db.ShootingRanges.Add(Convert(shootingRange));
                    db.SaveChanges();
                }
                catch (Exception exc)
                {
                    res = false;
                    throw new Exception("При добавлении тира произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="regionId">ид. регоина</param>
        /// <returns></returns>
        public List<ShootingRangeParams> GetByRegion(int? regionId)
        {
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.ShootingRanges.AsQueryable();
                    if (regionId.HasValue)
                    {
                        query = query.Where(x => x.IdRegion == regionId).OrderBy(x => x.Town).ThenBy(x => x.Name);
                    }

                    return query.ToList().ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При добавлении тира произошла ошибка");
                }
            }
        }
    }
}