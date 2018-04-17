using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблице ShooterClubs
    /// </summary>
    public class ShooterClubRepository
    {
        private ShooterClubParams Convert(ShooterClubs dalShooterClubs)
        {
            return new ShooterClubParams
            {
                Address = dalShooterClubs.Address ?? "",
                Id = dalShooterClubs.IdClub,
                IdShootingRange = dalShooterClubs.IdShootingRange,
                MainCoach = dalShooterClubs.MainCoach ?? "",
                Name = dalShooterClubs.Name,
                Phone = dalShooterClubs.Phone ?? "",
                UsId = dalShooterClubs.IdUser,
                DateCreate = dalShooterClubs.DateCreate
            };
        }

        private ShooterClubs Convert(ShooterClubParams shooterClub)
        {
            return new ShooterClubs
            {
                Address = shooterClub.Address,
                IdClub = shooterClub.Id,
                IdShootingRange = shooterClub.IdShootingRange,
                MainCoach = shooterClub.MainCoach,
                Name = shooterClub.Name,
                Phone = shooterClub.Phone,
                DateCreate = shooterClub.DateCreate,
                IdUser = shooterClub.UsId
            };
        }

        /// <summary>
        /// Добавить стрелковый клуб
        /// </summary>
        /// <param name="club"></param>
        /// <returns></returns>
        public ResultInfo Add(ShooterClubParams club)
        {
            var res = new ResultInfo();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    db.ShooterClubs.Add(Convert(club));
                    db.SaveChanges();
                }
                catch (Exception exc)
                {
                    res.IsOk = false;
                    res.Exc = exc;
                    res.ErrorMessage = "При добавлении стрелкового клуба произошла ошибка";
                }
            }

            return res;
        }

        public void Update(int clubId, ShooterClubParams shooterClub)
        {
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var updating = db.ShooterClubs.Where(x => x.IdClub == clubId).First();
                    updating.Address = shooterClub.Address;
                    updating.IdShootingRange = shooterClub.IdShootingRange;
                    updating.MainCoach = shooterClub.MainCoach;
                    updating.Name = shooterClub.Name;
                    updating.Phone = shooterClub.Phone;
                    updating.IdUser = shooterClub.UsId;
                    updating.DateCreate = shooterClub.DateCreate;
                }
                catch (Exception exc)
                {
                    throw new Exception("При обновлении стрелкового клуба произошла ошибка");
                }
            }
        }

        /// <summary>
        /// Получить стрелковый клуб
        /// </summary>
        /// <param name="id">ид. стрелкового клуба</param>
        /// <returns></returns>
        public ShooterClubParams Get(int id)
        {
            ShooterClubParams shooterClub;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    shooterClub = Convert(db.ShooterClubs.Where(x => x.IdClub == id).First());
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении стрелкового клуба произошла ошибка");
                }
            }

            return shooterClub;
        }

        /// <summary>
        /// Получить по названию
        /// </summary>
        /// <param name="name">название</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByName(string name)
        {
            var res = new List<ShooterClubParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    res = db.ShooterClubs.Where(x => x.Name == name).ToList().ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковый клубов произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить стрелковый клуб
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetByRegion(int idRegion)
        {
            var res = new List<ShooterClubDetalisationParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shooterClub in db.ShooterClubs
                                 join shooterRange in db.ShootingRanges on shooterClub.IdShootingRange equals shooterRange.Id
                                 join region in db.Regions on shooterRange.IdRegion equals region.IdRegion
                                 where shooterRange.IdRegion == idRegion
                                 select new { shooterClub, region.Name }).ToList();
                    foreach (var item in query)
                    {
                        res.Add(new ShooterClubDetalisationParams
                        {
                            Club = Convert(item.shooterClub),
                            RegionName = item.Name
                        });
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковых клубов по региону произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить стрелковый клуб
        /// </summary>
        /// <param name="idCountry">ид. страны</param>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetByCountry(int idCountry)
        {
            var res = new List<ShooterClubDetalisationParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shooterClub in db.ShooterClubs
                                 join shooterRange in db.ShootingRanges on shooterClub.IdShootingRange equals shooterRange.Id
                                 join region in db.Regions on shooterRange.IdRegion equals region.IdRegion
                                 where region.IdCountry == idCountry
                                 select new { shooterClub, region.Name }).ToList();
                    foreach (var item in query)
                    {
                        res.Add(new ShooterClubDetalisationParams
                        {
                            Club = Convert(item.shooterClub),
                            RegionName = item.Name
                        });
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковых клубов по стране произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить все стрелковые клубы
        /// </summary>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetAll()
        {
            var res = new List<ShooterClubDetalisationParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from shooterClub in db.ShooterClubs
                                 join shooterRange in db.ShootingRanges on shooterClub.IdShootingRange equals shooterRange.Id
                                 join region in db.Regions on shooterRange.IdRegion equals region.IdRegion
                                 select new { shooterClub, region.Name }).ToList();
                    foreach (var item in query)
                    {
                        res.Add(new ShooterClubDetalisationParams
                        {
                            Club = Convert(item.shooterClub),
                            RegionName = item.Name
                        });
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковых клубов по стране произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить все клубы, учавствующие в соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByCup(int idCup)
        {
            var list = new List<ShooterClubParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = (from club in db.ShooterClubs
                                 join shooters in db.Shooters on club.IdClub equals shooters.IdClub
                                 join entry in db.EntryForCompetitions on shooters.IdShooter equals entry.IdShooter
                                 join cupCompetitionType in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetitionType.Id
                                 where cupCompetitionType.IdCup == idCup
                                 select club).Distinct().ToList();

                    list = query.ConvertAll(Convert);
                                
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Произошла ошибка при получении списка клубов");
            }

            return list;
        }

        /// <summary>
        /// Получить список стрелковых клубов по ид тира
        /// </summary>
        /// <param name="idShootnigRange">ид. тира</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByShootingRange(int idShootnigRange)
        {
            var res = new List<ShooterClubParams>();
            
                try
                {
                    using (var db = DBContext.GetContext())
                    {
                        res = db.ShooterClubs.Where(x => x.IdShootingRange == idShootnigRange).ToList().ConvertAll(Convert);
                    }
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка стрелковый клубов произошла ошибка");
                }
            

            return res;
        }

        /// <summary>
        /// Удалить стрелковый клуб
        /// </summary>
        /// <param name="idClub"></param>
        /// <returns></returns>
        public ResultInfo Delete(int idClub)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var deleting = db.ShooterClubs.Where(x => x.IdClub == idClub).Single();
                    db.ShooterClubs.Remove(deleting);
                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "Произошла ошибка при удалении стрелкового клуба";
            }

            return res;
        }
    }
}
