using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Класс для работы с таблицей ShootingRange
    /// </summary>
    public class ShootingRangeRepository : BaseRepository<ShootingRangeParams, ShootingRanges>
    {
        public IQueryable<ShootingRanges> GetQueryShootingRangesByRegion(ShootingCompetitionRequestsEntities db, int idRegion)
        {
            return idRegion > 0 ? db.ShootingRanges.Where(x => x.IdRegion == idRegion) : db.ShootingRanges;
        }

        protected override Func<ShootingRanges, int> GetPrimaryKeyValue {
            get { return (x) => x.Id; }
        }
        
        protected override ShootingRangeParams ConvertToModel(ShootingRanges entity)
        {
            return new ShootingRangeParams
            {
                Address = entity.Address,
                Id = entity.Id,
                IdRegion = entity.IdRegion,
                Info = entity.Info,
                Name = entity.Name,
                Phone = entity.Telefon,
                Town = entity.Town,
                IdUser = entity.IdUser
            };
        }

        protected override ShootingRanges ConvertToEntity(ShootingRangeParams model)
        {
            return new ShootingRanges
            {
                Id = model.Id,
                Address = model.Address,
                IdRegion = model.IdRegion,
                Info = model.Info ?? "",
                Name = model.Name,
                Telefon = model.Phone ?? "",
                Town = model.Town ?? "",
                IdUser = model.IdUser
            };
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

                    return query.ToList().Select(ConvertToModel).ToList();
                }
                catch (Exception exc)
                {
                    throw new Exception("При добавлении тира произошла ошибка");
                }
            }
        }
    }
}