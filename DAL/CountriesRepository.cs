using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CountriesRepository
    {
        private CountryParams Convert(Countries country)
        {
            return new CountryParams
            {
                Id = country.Id,
                CountryName = country.CountryName,
                Code = country.CODE
            };
        }

        /// <summary>
        /// Получить страну по идентификатору
        /// </summary>
        /// <param name="id">ид</param>
        /// <returns></returns>
        public ResultInfoRef<CountryParams> GetCountryById(int id)
        {
            var res = new ResultInfoRef<CountryParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res.Data = Convert(db.Countries.Single(x => x.Id == id));
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Произошла ошибка при получении страны";
                res.Result.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Получить все страны
        /// </summary>
        /// <returns></returns>
        public ResultInfoRef<List<CountryParams>> GetAll()
        {
            var res = new ResultInfoRef<List<CountryParams>>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res.Data = db.Countries.ToList().ConvertAll(Convert);
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Произошла ошибка при получении страны";
                res.Result.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Получить страну по региону
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public ResultInfoRef<CountryParams> GetCountryByRegion(int idRegion)
        {
            var res = new ResultInfoRef<CountryParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res.Data = Convert((from region in db.Regions
                                    join country1 in db.Countries on region.IdCountry equals country1.Id
                                    where region.IdRegion == idRegion
                               select country1).Single());
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Произошла ошибка при получении страны";
                res.Result.Exc = exc;
            }

            return res;
        }
    }
}
