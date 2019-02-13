using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблице ShooterClubs
    /// </summary>
    public class ShooterClubRepository : BaseRepository<ShooterClubParams, ShooterClubs>
    {
        protected override Func<ShooterClubs, int> GetPrimaryKeyValue
        {
            get { return (x) => x.IdClub; }
        }

        protected override ShooterClubParams ConvertToModel(ShooterClubs entity)
        {
            return new ShooterClubParams
            {
                Address = entity.Address ?? "",
                Id = entity.IdClub,
                IdShootingRange = entity.IdShootingRange,
                MainCoach = entity.MainCoach ?? "",
                Name = entity.Name,
                Phone = entity.Phone ?? "",
                CreatorId = entity.IdUser,
                DateCreate = entity.DateCreate
            };
        }

        protected override ShooterClubs ConvertToEntity(ShooterClubParams model)
        {
            return new ShooterClubs
            {
                Address = model.Address,
                IdClub = model.Id,
                IdShootingRange = model.IdShootingRange,
                MainCoach = model.MainCoach,
                Name = model.Name,
                Phone = model.Phone,
                DateCreate = model.DateCreate,
                IdUser = model.CreatorId
            };
        }

        /// <summary>
        /// Получить по названию
        /// </summary>
        /// <param name="name">название</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByName(string name)
        {
            using (var db = DBContext.GetContext())
            {
                try
                {
                    return db.ShooterClubs.Where(x => x.Name == name).ToList().Select(ConvertToModel).ToList();
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковый клубов произошла ошибка");
                }
            }
        }

        private IQueryable<ClubDetailedEntity> GetDetailedQuery(ShootingCompetitionRequestsEntities db)
        {
            return from shooterClub in db.ShooterClubs
                join shooterRange in db.ShootingRanges on shooterClub.IdShootingRange equals shooterRange.Id
                join region in db.Regions on shooterRange.IdRegion equals region.IdRegion
                select new ClubDetailedEntity { Club = shooterClub, Region = region};
        }

        private ShooterClubDetalisationParams ConvertToDetailedModel(ClubDetailedEntity entity)
        {
            return new ShooterClubDetalisationParams
            {
                Id = entity.Club.IdClub,
                Name = entity.Club.Name,
                IdShootingRange = entity.Club.IdShootingRange,
                MainCoach = entity.Club.MainCoach,
                CreatorId = entity.Club.IdUser,
                Address = entity.Club.Address,
                Phone = entity.Club.Phone,
                DateCreate = entity.Club.DateCreate,
                RegionName = entity.Region.Name
            };
        }

        public List<ShooterClubDetalisationParams> GetDetailed(int? regionId, int? countryId)
        {
            using (var db = DBContext.GetContext())
            {
                var query = GetDetailedQuery(db);
                if (countryId.HasValue)
                {
                    query = query.Where(x => x.Region.IdCountry == countryId);
                }
                
                if (regionId.HasValue)
                {
                    query = query.Where(x => x.Region.IdRegion == regionId);
                }

                return query.ToList().Select(ConvertToDetailedModel).ToList();
            }
        }

        /// <summary>
        /// Получить все клубы, учавствующие в соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByCup(int idCup)
        {
            using (var db = DBContext.GetContext())
            {
                var items = (from club in db.ShooterClubs
                    join shooters in db.Shooters on club.IdClub equals shooters.IdClub
                    join entry in db.EntryForCompetitions on shooters.IdShooter equals entry.IdShooter
                    join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                    where cupCompetitionType.IdCup == idCup
                    select club).Distinct().ToList();

                return items.Select(ConvertToModel).ToList();
            }
        }

        /// <summary>
        /// Получить список стрелковых клубов по ид тира
        /// </summary>
        /// <param name="idShootnigRange">ид. тира</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByShootingRange(int idShootnigRange)
        {
            using (var db = DBContext.GetContext())
            {
                return db.ShooterClubs.Where(x => x.IdShootingRange == idShootnigRange).ToList().Select(ConvertToModel).ToList();
            }
        }
    }
}