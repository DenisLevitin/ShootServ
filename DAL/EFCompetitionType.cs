using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EFCompetitionType
    {
        private CompetitionType Convert(CompetitionTypeParams compType)
        {
            return new CompetitionType
            {
                Id = compType.Id,
                Name = compType.Name,
                SeriesCount = compType.SeriesCount,
                IdWeaponType = compType.IdWeaponType
            };
        }

        private CompetitionTypeParams Convert(CompetitionType compType)
        {
            return new CompetitionTypeParams
            {
                Id = compType.Id,
                Name = compType.Name,
                SeriesCount = compType.SeriesCount ?? 0,
                IdWeaponType = compType.IdWeaponType
            };
        }

        /// <summary>
        /// Получить по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CompetitionTypeParams Get(int id)
        {
            CompetitionTypeParams compType;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    compType = Convert(db.CompetitionType.Where(x => x.Id == id).Single());
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении упражнения произошла ошибка");
                }
            }

            return compType;
        }

        /// <summary>
        /// Получить по типу оружия
        /// </summary>
        /// <param name="idWeaponType"></param>
        /// <returns></returns>
        public List<CompetitionTypeParams> GetByWeaponType(int idWeaponType)
        {
            var res = new List<CompetitionTypeParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.CompetitionType.Where(x => x.IdWeaponType == idWeaponType).ToList();
                    res = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка упражнений произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить все упражнения
        /// </summary>
        /// <returns></returns>
        public List<CompetitionTypeParams> GetAll()
        {
            var res = new List<CompetitionTypeParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.CompetitionType.ToList();
                    res = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка упражнений произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить все упражнения по соревнованию
        /// </summary>
        /// <returns></returns>
        public List<CompetitionTypeParams> GetByCup(int idCup)
        {
            var res = new List<CompetitionTypeParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from competitionType in db.CompetitionType
                                 join cupCompetitionType in db.CupCompetitionType on competitionType.Id equals cupCompetitionType.IdCompetitionType
                                 where cupCompetitionType.IdCup == idCup
                                 select competitionType).ToList();
                    res = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении списка упражнений произошла ошибка");
                }
            }

            return res;
        }

        /// <summary>
        /// Добавить упражнение
        /// </summary>
        /// <param name="compType"></param>
        /// <returns></returns>
        public ResultInfoStruct<int> Add(CompetitionTypeParams compType)
        {
            var res = new ResultInfoStruct<int>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var adding = Convert(compType);
                    db.CompetitionType.Add(adding);
                    db.SaveChanges();

                    res.Data = adding.Id;
                }
                catch (Exception exc)
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "Не удалось добавить упражнение в базу";
                    res.Result.Exc = exc;
                }
            }

            return res;
        }

        /// <summary>
        /// Получить детализацию упражнений на соревновании с состоянием о заявке стрелков
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idUser">ид. пользователя</param>
        /// <returns></returns>
        public List<CupShooterCompetitionParams> GetCupCompetitionListWithShooterEntryDetails(int idCup, int idUser)
        {
            var res = new List<CupShooterCompetitionParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res = (from competition in db.CompetitionType
                                join cupCompetition in db.CupCompetitionType on competition.Id equals cupCompetition.IdCompetitionType
                                join cup in db.Cups on cupCompetition.IdCup equals cup.IdCup
                                from entry in
                                    (from entry in db.EntryForCompetitions
                                     join shooter in db.Shooters on entry.IdShooter equals shooter.IdShooter
                                     join user in db.Users on shooter.IdUser equals user.Id
                                     where user.Id == idUser
                                     select entry).Where(x => x.IdCupCompetitionType == cupCompetition.Id).DefaultIfEmpty()

                                where cup.IdCup == idCup
                                select new CupShooterCompetitionParams
                                {
                                    IdCompetitionType = competition.Id,
                                    IdCupCompetitionType = cupCompetition.Id,
                                    IsShooterWasEntried = entry.IdEntryStatus != null,
                                    NameCompetition = competition.Name,
                                    TimeFirstShift = cupCompetition.TimeFirstShift ?? default(DateTime)
                                }).ToList();
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Произошла ошибка при вызове GetCupCompetitionListWithShooterEntryDetails");
            }

            return res;
        }
    }
}
