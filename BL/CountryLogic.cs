using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
