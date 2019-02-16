﻿using BO;
using DAL;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// БЛ для работы с регионами
    /// </summary>
    public class RegionsLogic
    {
        private readonly RegionRepository _dalReginos;

        public RegionsLogic()
        {
            _dalReginos = new RegionRepository();
        }

        /// <summary>
        /// Получить список всех регионов
        /// </summary>
        public List<RegionParams> GetByCountry(int? idCountry)
        {
            return idCountry.HasValue ? _dalReginos.GetByCountry(idCountry.Value) : _dalReginos.GetAll();
        }

        /// <summary>
        /// Получить регион по ид.
        /// </summary>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public RegionParams Get(int idRegion)
        {
            return _dalReginos.Get(idRegion);
        }

        /// <summary>
        /// Получить регион по клубу
        /// </summary>
        /// <param name="idClub">ид. клуба</param>
        /// <returns></returns>
        public RegionParams GetRegionByClub(int idClub)
        {
            return _dalReginos.GetRegionByClub(idClub);
        }

        /// <summary>
        /// Получить регион по тиру
        /// </summary>
        /// <param name="idShootingRange">ид. тира</param>
        /// <returns></returns>
        public RegionParams GetRegionByShootingRange(int idShootingRange)
        {
            return _dalReginos.GetRegionByShootingRange(idShootingRange);
        }
    }
}
