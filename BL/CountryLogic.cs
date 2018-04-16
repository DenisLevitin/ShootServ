using BO;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class CountryLogic
    {
        private readonly EFCountries _dalCountries;

        public CountryLogic()
        {
            _dalCountries = new EFCountries();
        }

        /// <summary>
        /// Получить все страны
        /// </summary>
        /// <returns></returns>
        public ResultInfoRef<List<CountryParams>> GetAllCounties()
        {
            return _dalCountries.GetAll();
        }

        /// <summary>
        /// Получить страну по региону
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public ResultInfoRef<CountryParams> GetCountryByRegion(int idRegion)
        {
            return _dalCountries.GetCountryByRegion(idRegion);
        }
    }
}
