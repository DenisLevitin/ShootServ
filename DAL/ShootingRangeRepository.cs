using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common.CommandTrees;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Класс для работы с таблицей ShootingRange
    /// </summary>
    public class ShootingRangeRepository : BaseRepository<ShootingRangeParams, ShootingRanges>
    {
        protected override Func<ShootingRanges, int> GetPrimaryKeyValue
        {
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
        public List<ShootingRangeListItem> GetByRegion(int? regionId)
        {
            using (var db = DBContext.GetContext())
            {
                var query = from sr in db.ShootingRanges
                    join creator in db.Users on sr.IdUser equals creator.Id
                    join region in db.Regions on sr.IdRegion equals region.IdRegion
                    select new ShootingRangeListItem()
                    {
                        Id = sr.Id,
                        Address = sr.Address,
                        Name = sr.Name,
                        IdRegion = sr.IdRegion,
                        Info = sr.Info,
                        Phone = sr.Telefon,
                        RegionName = region.Name,
                        Town = sr.Town,
                        Creator = new UserParams()
                        {
                            DateCreate = creator.DateCreate,
                            Email = creator.E_mail,
                            FamilyName = creator.FatherName,
                            FatherName = creator.FatherName,
                            Name = creator.Name
                        }
                    };
                
                if (regionId.HasValue)
                {
                    query = query.Where(x => x.IdRegion == regionId);
                }

                return query.OrderBy(x => x.Town).ThenBy(x => x.Name).ToList();
            }
        }
    }
}