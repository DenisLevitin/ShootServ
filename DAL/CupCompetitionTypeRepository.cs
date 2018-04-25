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
    public class CupCompetitionTypeRepository : BaseRepository<CupCompetitionTypeParams, CupCompetitionType>
    {
        protected override Func<CupCompetitionType, int> GetPrimaryKeyValue => (x) => { return x.Id; };

        protected override CupCompetitionTypeParams ConvertToModel(CupCompetitionType cupCompType)
        {
            return new CupCompetitionTypeParams
            {
                Id = cupCompType.Id,
                IdCompetitionType = cupCompType.IdCompetitionType,
                IdCup = cupCompType.IdCup,
                TimeFirstShift = cupCompType.TimeFirstShift
            };
        }

        protected override CupCompetitionType ConvertToEntity(CupCompetitionTypeParams cupCompType)
        {
            return new CupCompetitionType
            {
                Id = cupCompType.Id,
                IdCompetitionType = cupCompType.IdCompetitionType,
                IdCup = cupCompType.IdCup,
                TimeFirstShift = cupCompType.TimeFirstShift
            };
        }

        /// <summary>
        /// Получить список упржанений по соревнованию
        /// </summary>
        /// <param name="idCup"></param>
        /// <returns></returns>
        public List<CupCompetitionTypeParams> GetByCup(int idCup)
        {
            return GetFiltered(x => x.IdCup == idCup);
        }

        /// <summary>
        /// Получить упражнения по соревнованию, на которые есть заявки
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<CupCompetitionTypeParams> GetByCupWithEntry(int idCup)
        {
            List<CupCompetitionTypeParams> res;

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = (from entry in db.EntryForCompetitions
                                 join cupCompetition in db.CupCompetitionType on entry.IdCupCompetitionType equals cupCompetition.Id
                                 where cupCompetition.IdCup == idCup
                                 select cupCompetition).ToList();
                    res = query.Select(ConvertToModel).ToList();
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
        public CupCompetitionTypeParams GetByCupAndCompType(int idCup, int idCompType)
        {
            return GetFiltered(x => x.IdCup == idCup && x.IdCompetitionType == idCompType).FirstOrDefault();
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
                        var delItem = db.CupCompetitionType.FirstOrDefault(x => x.Id == item.Id);
                        if (delItem != null)
                        {
                            db.CupCompetitionType.Remove(delItem);
                        }
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
