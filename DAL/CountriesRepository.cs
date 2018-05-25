using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CountriesRepository : BaseRepository<CountryParams, Countries>
    {
        protected override Func<Countries, int> GetPrimaryKeyValue { get { return x => x.Id; } }

        protected override CountryParams ConvertToModel(Countries country)
        {
            return new CountryParams
            {
                Id = country.Id,
                CountryName = country.CountryName,
                Code = country.CODE
            };
        }

        protected override Countries ConvertToEntity(CountryParams country)
        {
            return new Countries
            {
                Id = country.Id,
                CountryName = country.CountryName,
                CODE = country.Code
            };
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
                    res.Data = ConvertToModel((from region in db.Regions
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
