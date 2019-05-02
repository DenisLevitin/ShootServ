using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблицей Regions
    /// </summary>
    public class RegionRepository : BaseRepository<RegionParams, Regions>
    {
        protected override Func<Regions, int> GetPrimaryKeyValue => x => x.IdRegion;

        /// <summary>
        /// Получить список регионов
        /// </summary>
        /// <returns></returns>
        public List<RegionParams> GetByCountry(int? idCountry)
        {
            return idCountry.HasValue ? GetFiltered(x => x.IdCountry == idCountry.Value) : GetAll();
        }

        /// <summary>
        /// Получить регион по тиру
        /// </summary>
        /// <param name="idShootingRange">ид. тира</param>
        /// <returns></returns>
        public RegionParams GetRegionByShootingRange(int idShootingRange)
        {
            return GetFirstOrDefault(x => x.ShootingRanges.Any(y => y.Id == idShootingRange));
        }

        /// <summary>
        /// Получить регион по команде
        /// </summary>
        /// <param name="idClub">ид. команды (стрелкового клуба)</param>
        /// <returns></returns>
        public RegionParams GetRegionByClub(int idClub)
        {
            return GetFirstOrDefault(x => x.ShootingRanges.SelectMany(sr => sr.ShooterClubs).Any(y => y.IdClub == idClub));
        }

        protected override RegionParams ConvertToModel(Regions entity)
        {
            return new RegionParams
            {
                Id = entity.IdRegion,
                Name = entity.Name
            };
        }

        protected override Regions ConvertToEntity(RegionParams model)
        {
            throw new NotImplementedException();
        }
    }
}
