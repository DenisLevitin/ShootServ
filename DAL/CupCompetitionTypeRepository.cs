using System.Data;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL класс для работы таблицей CupCompetitionType
    /// </summary>
    public class CupCompetitionTypeRepository
    {
        private CupCompetitionTypeParams Convert(CupCompetitionType cupCompType)
        {
            return new CupCompetitionTypeParams
            {
                Id = cupCompType.Id,
                IdCompetitionType = cupCompType.IdCompetitionType,
                IdCup = cupCompType.IdCup,
                TimeFirstShift = cupCompType.TimeFirstShift
            };
        }

        private CupCompetitionType Convert(CupCompetitionTypeParams cupCompType)
        {
            return new CupCompetitionType
            {
                Id = cupCompType.Id,
                IdCompetitionType = cupCompType.IdCompetitionType,
                IdCup = cupCompType.IdCup,
                TimeFirstShift = cupCompType.TimeFirstShift
            };
        }

        public ResultInfo Add(CupCompetitionTypeParams cupCompType)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    db.CupCompetitionType.Add(Convert(cupCompType));
                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "Произошла ошибка при добавлении записи в таблицу CupCompetitionType";
                res.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Обновить упражнение на соревновании
        /// </summary>
        /// <param name="id">ид. упражнения на соревновании</param>
        /// <param name="cup">соревнование</param>
        /// <returns></returns>
        public ResultInfo Update(int id, CupCompetitionTypeParams cup)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var updating = new CupCompetitionType { Id = id};
                    db.CupCompetitionType.Attach(updating);

                    updating.IdCompetitionType = cup.IdCompetitionType;
                    updating.IdCup = cup.IdCup;
                    updating.TimeFirstShift = cup.TimeFirstShift;

                    db.Entry(updating).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Произошла ошибка при получении списка упражнений на соревновании");
            }

            return res;
        }

        /// <summary>
        /// Получить список упржанений по соревнованию
        /// </summary>
        /// <param name="idCup"></param>
        /// <returns></returns>
        public List<CupCompetitionTypeParams> GetByCup(int idCup)
        {
            var list = new List<CupCompetitionTypeParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = db.CupCompetitionType.Where(x => x.IdCup == idCup).ToList();
                    list = query.ConvertAll(Convert);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Произошла ошибка при получении списка упражнений на соревновании");
            }

            return list;
        }

        /// <summary>
        /// Получить упражнения по соревнованию, на которые есть заявки
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<CupCompetitionTypeParams> GetByCupWithEntry(int idCup)
        {
            var res = new List<CupCompetitionTypeParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = (from entry in db.EntryForCompetitions
                                 join cupCompetition in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetition.Id
                                 where cupCompetition.IdCup == idCup
                                 select cupCompetition).ToList();
                    res = query.ConvertAll(Convert);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("При получении списка упражнений произошла ошибка");
            }

            return res;
        }

        /// <summary>
        /// Получить упражнение по типу и соревнованию
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idCompType">ид. типа</param>
        /// <returns></returns>
        public CupCompetitionType GetByCupAndCompType(int idCup, int idCompType)
        {
            CupCompetitionType res;

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res = db.CupCompetitionType.Single(x => x.IdCup == idCup && x.IdCompetitionType == idCompType);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Произошла ошибка при получении CupCompetitionType");
            }

            return res;
        }

        /// <summary>
        /// Добавить список
        /// </summary>
        /// <param name="adding">добавляемые</param>
        /// <returns></returns>
        public ResultInfo AddRange(List<CupCompetitionTypeParams> adding)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var dalAdding = adding.ConvertAll(Convert).ToList();

                    foreach (var item in dalAdding)
                    {
                        db.CupCompetitionType.Add(item);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "Произошла ошибка при добавлении записи в таблицу CupCompetitionType";
                res.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Удалить список
        /// </summary>
        /// <param name="deleting">добавляемые</param>
        /// <returns></returns>
        public ResultInfo DelRange(List<CupCompetitionTypeParams> deleting)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {

                    foreach (var item in deleting)
                    {
                        var delItem = db.CupCompetitionType.Where(x => x.Id == item.Id).Single();
                        db.CupCompetitionType.Remove(delItem);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "Произошла ошибка при удалении записи из таблицы CupCompetitionType";
                res.Exc = exc;
            }

            return res;
        }
    }
}
