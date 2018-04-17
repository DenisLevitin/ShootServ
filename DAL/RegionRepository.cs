using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблицей Regions
    /// </summary>
    public class RegionRepository
    {
        private RegionParams Convert(Regions dalRegion)
        {
            return new RegionParams
            {
                Id = dalRegion.IdRegion,
                Name = dalRegion.Name
            };
        }

        /// <summary>
        /// Получить регион по идентификатору
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public RegionParams Get(int idRegion)
        {
            RegionParams region = new RegionParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    region = Convert(db.Regions.Where(x => x.IdRegion == idRegion).FirstOrDefault());
                }
                catch (Exception exc)
                {
                    //throw new Exception("При получении региона по идентификатору произошла ошибка");
                }
            }

            return region;
        }

        /// <summary>
        /// Получить список регионов
        /// </summary>
        /// <returns></returns>
        public ResultInfoRef<List<RegionParams>> GetAll()
        {
            var res = new ResultInfoRef<List<RegionParams>>();

            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.Regions.OrderBy(x=>x.Name).ToList();
                    res.Data = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "При получении регионов произошла ошиибка";
                    res.Result.Exc = exc;
                }
            }

            return res;
        }

        /// <summary>
        /// Получить список регионов
        /// </summary>
        /// <returns></returns>
        public ResultInfoRef<List<RegionParams>> GetByCountry(int idCountry)
        {
            var res = new ResultInfoRef<List<RegionParams>>();

                try
                {
                    using (var db = DBContext.GetContext())
                    {
                        var query = (from region in db.Regions
                                     join country in db.Countries on region.IdCountry equals country.Id
                                     where country.Id == idCountry
                                     select region).ToList();

                        res.Data = query.ConvertAll(Convert);
                    }
                }
                catch (Exception exc)
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "При получении региона по стране произошла ошибка";
                    res.Result.Exc = exc;
                }
            

            return res;
        }

        /// <summary>
        /// Получить регион по тиру
        /// </summary>
        /// <param name="idShootingRange">ид. тира</param>
        /// <returns></returns>
        public RegionParams GetRegionByShootingRange(int idShootingRange)
        {
            RegionParams res = new RegionParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shootingRange1 in db.ShootingRanges
                                 join region in db.Regions on shootingRange1.IdRegion equals region.IdRegion
                                 where shootingRange1.Id == idShootingRange
                                 select region).Distinct().First();
                    res = Convert(query);
                }
                catch (Exception exc)
                {
                    //throw new Exception("При получении региона по идентификатору произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить регион по команде
        /// </summary>
        /// <param name="idClub">ид. команды (стрелкового клуба)</param>
        /// <returns></returns>
        public RegionParams GetRegionByClub(int idClub)
        {
            RegionParams res = new RegionParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from club in db.ShooterClubs
                                join shootingRange1 in db.ShootingRanges on club.IdShootingRange equals shootingRange1.Id
                                join region in db.Regions on shootingRange1.IdRegion equals region.IdRegion
                                where club.IdClub == idClub
                                select region).Distinct().First();
                    res = Convert(query);
                }
                catch (Exception exc)
                {
                    //throw new Exception("При получении региона по идентификатору произошла ошибка");
                }
            }

            return res;
        }
    }
}
