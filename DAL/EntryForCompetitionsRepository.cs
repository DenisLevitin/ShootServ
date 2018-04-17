using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы с таблицей EntryForCompetitions
    /// </summary>
    public class EntryForCompetitionsRepository
    {
        private EntryForCompetitionsParams Convert(EntryForCompetitions entry)
        {
            return new EntryForCompetitionsParams
            {
                Id = entry.Id,
                IdCupCompetitionType = entry.IdCupCompetitionType,
                IdShooter = entry.IdShooter,
                IdEntryStatus = entry.IdEntryStatus,
                DateCreate = entry.DateCreate
            };
        }

        private EntryForCompetitions Convert(EntryForCompetitionsParams entry)
        {
            return new EntryForCompetitions
            {
                Id = entry.Id,
                IdCupCompetitionType = entry.IdCupCompetitionType,
                IdShooter = entry.IdShooter,
                IdEntryStatus = entry.IdEntryStatus,
                DateCreate = entry.DateCreate
            };
        }

        /// <summary>
        /// Добавить заявку
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public ResultInfo Add(EntryForCompetitionsParams entry)
        {
            var res = new ResultInfo();

                try
                {
                    using (var db = DBContext.GetContext())
                    {
                        var adding = Convert(entry);
                        db.EntryForCompetitions.Add(adding);
                        db.SaveChanges();
                    }
                }
                catch (Exception exc)
                {
                    res.IsOk = false;
                    res.ErrorMessage = "При добавлении заявки в базу произошла ошибка";
                    res.Exc = exc;
                }
            

            return res;
        }

        /// <summary>
        /// Обновить заявку
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public ResultInfo Update(int idEntry, EntryForCompetitionsParams entry)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var update = Convert(entry);
                    var updating = new EntryForCompetitions();

                    updating.Id = idEntry;
                    updating.IdCupCompetitionType = update.IdCupCompetitionType;
                    updating.IdEntryStatus = update.IdEntryStatus;
                    updating.IdShooter = updating.IdShooter;
                    updating.DateCreate = updating.DateCreate;

                    db.EntryForCompetitions.Attach(updating);
                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "При обновлении заявки произошла ошибка";
                res.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Получить заявку по идентификатору
        /// </summary>
        /// <param name="id">ид. заявки</param>
        /// <returns></returns>
        public EntryForCompetitionsParams Get(int id)
        {
            EntryForCompetitionsParams res;
            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = db.EntryForCompetitions.Where(x => x.Id == id).Single();
                    res = Convert(query);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("При получении заявки по идентификатору произошла ошибка");
            }

            return res;
        }

        /// <summary>
        /// Получить заявки по соревнованию
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<EntryForCompetitionsParams> GetByCup(int idCup)
        {
            var res = new List<EntryForCompetitionsParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = (from entry in db.EntryForCompetitions
                                 join cupCompetition in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetition.Id
                                 where cupCompetition.IdCup == idCup
                                 select entry).ToList();
                    res = query.ConvertAll(Convert);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("При получении заявок по соревнованию произошла ошибка");
            }

            return res;
        }
    }
}
