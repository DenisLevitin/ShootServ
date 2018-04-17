using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// CupTypes table
    /// 
    /// </summary>
    public class CupTypeRepository
    {
        private CupTypeParams Convert(CupTypes cupType)
        {
            return new CupTypeParams
            {
                Id = cupType.Id,
                Name = cupType.Name,
                Keychar = cupType.Keychar
            };
        }

        /// <summary>
        /// Получить по ид.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CupTypeParams Get(int id)
        {
            CupTypeParams cupType;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    cupType = Convert(db.CupTypes.Where(x => x.Id == id).Single());
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении типа соревнования произошла ошибка");
                }
            }

            return cupType;
        }

        /// <summary>
        /// Получить по ид.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CupTypeParams> GetAll()
        {
            List<CupTypeParams> res;
            using (var db = DBContext.GetContext())
            {
                try
                {
                    res = db.CupTypes.ToList().ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("При получении типа соревнования произошла ошибка");
                }
            }

            return res;
        }
    }
}
