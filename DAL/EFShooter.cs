using System.Security.Cryptography;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Класс для работы с таблицей Shooters
    /// </summary>
    public class EFShooter
    {
        private ShooterParams Convert(Shooters dalShooter)
        {
            return new ShooterParams
            {
                Address = dalShooter.Address ?? "",
                BirthDate = dalShooter.BirthDate,
                Family = dalShooter.Family,
                FatherName = dalShooter.FatherName,
                Id = dalShooter.IdShooter,
                IdCategory = dalShooter.IdCategory,
                IdClub = dalShooter.IdClub,
                IdWeaponType= dalShooter.IdWeaponType, // поменять тип в базе с  Nullable
                Name = dalShooter.Name,
                Sex = System.Convert.ToInt32( dalShooter.Sex )
            };
        }

        private Shooters Convert(ShooterParams shooter)
        {
            return new Shooters
            {
                Address = shooter.Address,
                BirthDate = shooter.BirthDate,
                DateCreate = shooter.DateCreate,
                Name = shooter.Name,
                Family = shooter.Family,
                FatherName = shooter.FatherName,
                IdCategory = shooter.IdCategory,
                IdClub = shooter.IdClub,
                IdUser = shooter.IdUser,
                IdWeaponType = shooter.IdWeaponType,
                IdShooter = shooter.Id,
                Sex = System.Convert.ToBoolean(shooter.Sex)
            };
        }

        /// <summary>
        /// Добавить стрелка
        /// </summary>
        /// <param name="shooter">стрелок</param>
        public ResultInfo Add(ShooterParams shooter)
        {
            var res = new ResultInfo();
             using (var db = DBContext.GetContext())
             {
                try
                {
                    db.Shooters.Add(Convert(shooter));
                    db.SaveChanges();
                } 
                catch(Exception exc)
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Не удалось добавить стрелка в базу";
                    res.Exc = exc;
                }
             }

             return res;
        }

        /// <summary>
        /// Получить стрелка по Id
        /// </summary>
        /// <param name="idShooter"></param>
        /// <returns></returns>
        public ShooterParams GetById(int idShooter)
        {
            ShooterParams shooter;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    shooter = Convert(db.Shooters.Where(x => x.IdShooter == idShooter).First());
                }
                catch (Exception exc)
                {
                    throw new Exception("Не найден стрелок по идентификатору");
                }
            }

            return shooter;
        }

        /// <summary>
        /// Получить стрелка по Id
        /// </summary>
        /// <returns></returns>
        public ShooterParams GetByUser(int idUser)
        {
            ShooterParams res;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shooter in db.Shooters
                        where shooter.IdUser == idUser
                        select shooter).Single();

                    res = Convert(query);
                }
                catch (Exception exc)
                {
                    throw new Exception("Не найден стрелок по идентификатору пользователя");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить список стрелков по клубу
        /// </summary>
        /// <param name="clubId">ид. клуба</param>
        /// <returns></returns>
        public List<ShooterParams> GetByClub(int clubId) 
        {
            List<ShooterParams> shooters = new List<ShooterParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.Shooters.Where(x => x.IdClub == clubId).ToList();
                    shooters = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелков произошла ошибка");
                }
            }

            return shooters;
        }

        /// <summary>
        /// Получить список стрелков, заявленных на соревнование
        /// </summary>
        /// <param name="cupId">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterParams> GetShootersWasEntryOnCup(int cupId)
        {
            List<ShooterParams> shooters = new List<ShooterParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shooter in db.Shooters
                                 join entry in db.EntryForCompetitions on shooter.IdShooter equals entry.IdShooter
                                 join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                                 where cupCompetitionType.IdCup == cupId
                                 select shooter).ToList();
                    shooters = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелков произошла ошибка");
                }
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
            List<ShooterParams> shooters = new List<ShooterParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shooter in db.Shooters
                                 join entry in db.EntryForCompetitions on shooter.IdShooter equals entry.IdShooter
                                 join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                                 where cupCompetitionType.IdCup == cupId && shooter.IdClub == clubId
                                 select shooter).ToList();
                    shooters = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелков произошла ошибка");
                }
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

            try
            {
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
                                 group new { competitionType } by new { shooter, category, club, shootRange } into gr
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
            }
            catch (Exception exc)
            {
                throw new Exception("Не удалось получить список заявленных на соревнование стрелков");
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

            try
            {
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
                                 group new { competitionType } by new { shooter, category, club, shootRange } into gr
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
            }
            catch (Exception exc)
            {
                throw new Exception("Не удалось получить список заявленных на соревнование стрелков");
            }

            return res;
        }

        /// <summary>
        /// Обновить стрелка
        /// </summary>
        /// <param name="idShooter">ид. стрелка</param>
        /// <param name="shooter">стрелок</param>
        /// <returns></returns>
        public ResultInfo Update(int idShooter, ShooterParams shooter)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var updating = db.Shooters.Where(x => x.IdShooter == idShooter).Single();

                    updating.Address = shooter.Address;
                    updating.BirthDate = shooter.BirthDate;
                    updating.Family = shooter.Family;
                    updating.Name = shooter.Name;
                    updating.FatherName = shooter.FatherName;
                    updating.IdClub = shooter.IdClub;
                    updating.IdWeaponType = shooter.IdWeaponType;
                    updating.IdCategory = shooter.IdCategory;

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "Произошла ошибка при обновлении данных стрелка";
                res.Exc = exc;
            }

            return res;
        }
    }
}
