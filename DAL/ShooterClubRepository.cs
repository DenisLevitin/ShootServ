using BO;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    return db.ShooterClubs.Where(x => x.Name == name).ToList().Select(x => ConvertToModel(x)).ToList();
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковый клубов произошла ошибка");
                }
            }
        }

        /// <summary>
        /// Получить стрелковый клуб
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetByRegion(int idRegion)
        {
            using (var db = DBContext.GetContext())
            {
                var query = GetDetailedQuery(db);
                return query.Where(x => x.region.IdRegion == idRegion).ToList().Select(ConvertToDetailedModel).ToList();
            }
        }

        private IQueryable<(ShooterClubs shooterClub, Regions region)> GetDetailedQuery(ShootingCompetitionRequestsEntities db)
        {
            return from shooterClub in db.ShooterClubs
                join shooterRange in db.ShootingRanges on shooterClub.IdShootingRange equals shooterRange.Id
                join region in db.Regions on shooterRange.IdRegion equals region.IdRegion
                select System.ValueTuple.Create(shooterClub, region);
        }

        private ShooterClubDetalisationParams ConvertToDetailedModel((ShooterClubs shooterClub, Regions region) valueTuple)
        {
            return new ShooterClubDetalisationParams
            {
                Id = valueTuple.shooterClub.IdClub,
                Name = valueTuple.shooterClub.Name,
                IdShootingRange = valueTuple.shooterClub.IdShootingRange,
                MainCoach = valueTuple.shooterClub.MainCoach,
                CreatorId = valueTuple.shooterClub.IdUser,
                Address = valueTuple.shooterClub.Address,
                Phone = valueTuple.shooterClub.Phone,
                DateCreate = valueTuple.shooterClub.DateCreate,
                RegionName = valueTuple.region.Name
            };
        }

        /// <summary>
        /// Получить стрелковый клуб
        /// </summary>
        /// <param name="idCountry">ид. страны</param>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetByCountry(int idCountry)
        {
            using (var db = DBContext.GetContext())
            {
                var query = GetDetailedQuery(db);
                return query.Where(x => x.region.IdCountry == idCountry).ToList().Select(ConvertToDetailedModel).ToList();
            }
        }

        /// <summary>
        /// Получить все стрелковые клубы
        /// </summary>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetAllDetailed()
        {
            using (var db = DBContext.GetContext())
            {
                return GetDetailedQuery(db).ToList().Select(ConvertToDetailedModel).ToList();
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