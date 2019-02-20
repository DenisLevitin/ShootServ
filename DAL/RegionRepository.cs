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
            var region = new RegionParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    region = Convert(db.Regions.FirstOrDefault(x => x.IdRegion == idRegion));
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
        public List<RegionParams> GetAll()
        {
            using (var db = DBContext.GetContext())
            {
                var query = db.Regions.OrderBy(x => x.Name).ToList();
                return query.ConvertAll(Convert);
            }
        }

        /// <summary>
        /// Получить список регионов
        /// </summary>
        /// <returns></returns>
        public List<RegionParams> GetByCountry(int idCountry)
        {
            using (var db = DBContext.GetContext())
            {
                var query = (from region in db.Regions
                             join country in db.Countries on region.IdCountry equals country.Id
                             where country.Id == idCountry
                             select region).ToList();

                return query.ConvertAll(Convert);
            }
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
