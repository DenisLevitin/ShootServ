using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Класс для работы с таблицей Shooters
    /// </summary>
    public class ShooterRepository : BaseRepository<ShooterParams, Shooters>
    {
        protected override Func<Shooters, int> GetPrimaryKeyValue
        {
            get { return (x) => x.IdShooter; }
        }

        protected override ShooterParams ConvertToModel(Shooters entity)
        {
            return new ShooterParams
            {
                Address = entity.Address ?? "",
                BirthDate = entity.BirthDate,
                Family = entity.Family,
                FatherName = entity.FatherName,
                Id = entity.IdShooter,
                IdCategory = entity.IdCategory,
                IdClub = entity.IdClub,
                IdWeaponType = entity.IdWeaponType, // поменять тип в базе с  Nullable
                Name = entity.Name,
                Sex = System.Convert.ToInt32(entity.Sex)
            };
        }

        protected override Shooters ConvertToEntity(ShooterParams model)
        {
            return new Shooters
            {
                Address = model.Address,
                BirthDate = model.BirthDate,
                DateCreate = model.DateCreate,
                Name = model.Name,
                Family = model.Family,
                FatherName = model.FatherName,
                IdCategory = model.IdCategory,
                IdClub = model.IdClub,
                IdUser = model.IdUser,
                IdWeaponType = model.IdWeaponType,
                IdShooter = model.Id,
                Sex = System.Convert.ToBoolean(model.Sex)
            };
        }

        /// <summary>
        /// Получить стрелка по Id
        /// </summary>
        /// <returns></returns>
        public ShooterParams GetByUser(int idUser)
        {
            return GetFirstOrDefault(x => x.IdUser == idUser);
        }

        /// <summary>
        /// Получить список стрелков по клубу
        /// </summary>
        /// <param name="clubId">ид. клуба</param>
        /// <returns></returns>
        public List<ShooterParams> GetByClub(int clubId)
        {
            return GetFiltered(x => x.IdClub == clubId);
        }

        /// <summary>
        /// Получить список стрелков, заявленных на соревнование
        /// </summary>
        /// <param name="cupId">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterParams> GetShootersWasEntryOnCup(int cupId)
        {
            /// TODO: Сделать через запрос filtered

            var shooters = new List<ShooterParams>();
            using (var db = DBContext.GetContext())
            {
                var query = (from shooter in db.Shooters
                    join entry in db.EntryForCompetitions on shooter.IdShooter equals entry.IdShooter
                    join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                    where cupCompetitionType.IdCup == cupId
                    select shooter).ToList();
                shooters = query.ConvertAll(ConvertToModel);
            }

            return shooters;
        }

        /// <summary>
        /// Получить список стрелков, заявленных на соревнование от определенного клуба
        /// </summary>
        /// <param name="cupId">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterParams> GetShootersWasEntryOnCupInClub(int cupId, int clubId)
        {
            /// TODO: Сделать через запрос filtered
            
            var shooters = new List<ShooterParams>();
            using (var db = DBContext.GetContext())
            {
                var query = (from shooter in db.Shooters
                    join entry in db.EntryForCompetitions on shooter.IdShooter equals entry.IdShooter
                    join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                    where cupCompetitionType.IdCup == cupId && shooter.IdClub == clubId
                    select shooter).ToList();
                shooters = query.ConvertAll(ConvertToModel);
            }

            return shooters;
        }

        /// <summary>
        /// Получить заявленных стрелков на соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterEntryDetailsParams> GetEntryShootersOnCup(int idCup)
        {
            var res = new List<ShooterEntryDetailsParams>();
            using (var db = DBContext.GetContext())
            {
                var query = (from shooter in db.Shooters
                    join club in db.ShooterClubs on shooter.IdClub equals club.IdClub
                    join shootRange in db.ShootingRanges on club.IdShootingRange equals shootRange.Id
                    join entry in db.EntryForCompetitions on shooter.IdShooter equals entry.IdShooter
                    join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                    join competitionType in db.CompetitionType on cupCompetitionType.IdCompetitionType equals competitionType.Id
                    join category in db.ShooterCategory on shooter.IdCategory equals category.Id
                    where cupCompetitionType.IdCup == idCup
                    group new {competitionType} by new {shooter, category, club, shootRange}
                    into gr
                    select new
                    {
                        shooter = gr.Key.shooter,
                        category = gr.Key.category,
                        competitionType = gr.Select(x => x.competitionType),
                        club = gr.Key.club,
                        shootRange = gr.Key.shootRange
                    }).ToList();

                foreach (var item in query)
                {
                    res.Add(new ShooterEntryDetailsParams
                    {
                        IdShooter = item.shooter.IdShooter,
                        FamilyName = item.shooter.Family,
                        Name = item.shooter.Name,
                        FatherName = item.shooter.FatherName,
                        SexEnum = item.shooter.Sex ? SexEnum.Men : SexEnum.Women,
                        BirthDate = item.shooter.BirthDate,
                        ClubName = item.club.Name,
                        Category = item.category.Name,
                        Competitions = item.competitionType.Select(x => x.Name).ToList(),
                        Town = item.shootRange.Town,
                        ShootRangeName = item.shootRange.Name
                    });
                }
            }

            return res;
        }

        /// <summary>
        /// Получить заявленных стрелков на соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idClub">ид. команды</param>
        /// <returns></returns>
        public List<ShooterEntryDetailsParams> GetEntryShootersOnCupAndClub(int idCup, int idClub)
        {
            var res = new List<ShooterEntryDetailsParams>();

            using (var db = DBContext.GetContext())
            {
                var query = (from shooter in db.Shooters
                    join club in db.ShooterClubs on shooter.IdClub equals club.IdClub
                    join shootRange in db.ShootingRanges on club.IdShootingRange equals shootRange.Id
                    join entry in db.EntryForCompetitions on shooter.IdShooter equals entry.IdShooter
                    join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                    join competitionType in db.CompetitionType on cupCompetitionType.IdCompetitionType equals competitionType.Id
                    join category in db.ShooterCategory on shooter.IdCategory equals category.Id
                    where cupCompetitionType.IdCup == idCup && shooter.IdClub == idClub
                    group new {competitionType} by new {shooter, category, club, shootRange}
                    into gr
                    select new
                    {
                        shooter = gr.Key.shooter,
                        category = gr.Key.category,
                        competitionType = gr.Select(x => x.competitionType),
                        club = gr.Key.club,
                        shootRange = gr.Key.shootRange
                    }).ToList();

                foreach (var item in query)
                {
                    res.Add(new ShooterEntryDetailsParams
                    {
                        IdShooter = item.shooter.IdShooter,
                        FamilyName = item.shooter.Family,
                        Name = item.shooter.Name,
                        FatherName = item.shooter.FatherName,
                        SexEnum = item.shooter.Sex ? SexEnum.Men : SexEnum.Women, //item.shooter.Sex ? "мужской" : "женский",
                        BirthDate = item.shooter.BirthDate,
                        ClubName = item.club.Name,
                        Category = item.category.Name,
                        Competitions = item.competitionType.Select(x => x.Name).ToList(),
                        Town = item.shootRange.Town,
                        ShootRangeName = item.shootRange.Name
                    });
                }
            }

            return res;
        }
    }
}