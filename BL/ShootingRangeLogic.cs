using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    /// <summary>
    /// БЛ по работе с тиром
    /// </summary>
    public class ShootingRangeLogic
    {
        private readonly ShootingRangeRepository _dalShootingRange;
        private readonly UserLogic _userLogic;
        private readonly CupLogic _cupLogic;
        private readonly ShootingClubLogic _clubLogic;

        public ShootingRangeLogic()
        {
            _dalShootingRange = new ShootingRangeRepository();
            _userLogic = new UserLogic();
            _cupLogic = new CupLogic();
            _clubLogic = new ShootingClubLogic();
        }

        /// <summary>
        /// Удаляем тир
        /// </summary>
        /// <param name="idShootRange">ид. тира</param>
        /// <param name="idUser">ид. пользователя</param>
        /// <returns></returns>
        public ResultInfo Delete(int idShootRange, int idUser)
        {
            var res = new ResultInfo();

            var clubs = _clubLogic.GetByShootingRange(idShootRange);
            if (clubs.Count == 0)
            {
                var user = _userLogic.Get(idUser);
                if (user != null)
                {
                    var shootRange = _dalShootingRange.Get(idShootRange);

                    if (idUser == shootRange.IdUser)
                    {
                        var cups = _cupLogic.GetByShootingRangeAndDates(idShootRange, new DateTime(1970, 1, 1), new DateTime(2080, 1, 1));
                        if (cups.Count == 0)
                        {
                            _dalShootingRange.Delete(idShootRange);
                        }
                        else
                        {
                            res.IsOk = false;
                            res.ErrorMessage = "Нельзя удалить тир, т.к к нему привязаны соревнования";
                        }
                    }
                    else
                    {
                        res.IsOk = false;
                        res.ErrorMessage = "Пользователь не может удалить тир, т.к не он его добавил";
                    }
                }
                else
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Ошибка определения пользователя";
                }
            }
            else
            {
                res.IsOk = false;
                res.ErrorMessage = "Нельзя удалить тир, к которому привязан стрелковый клуб";
            }

            return res;
        }

        /// <summary>
        /// Добавить тир
        /// </summary>
        /// <param name="shootingRange">тир</param>
        /// <param name="userId">ид. пользователя</param>
        /// <returns></returns>
        public ResultInfo Add(ShootingRangeParams shootingRange, int userId)
        {
            var res = new ResultInfo();
            if (string.IsNullOrWhiteSpace(shootingRange.Name))
            {
                res.IsOk = false;
                res.ErrorMessage = "Нельзя добавить тир без названия";
                return res;
            }

            if (string.IsNullOrWhiteSpace(shootingRange.Address))
            {
                res.IsOk = false;
                res.ErrorMessage = "Нельзя добавить тир без адреса";
            }

            var queryUser = _userLogic.Get(userId);
            if (queryUser != null)
            {
                var allInRegions = _dalShootingRange.GetByRegion(shootingRange.IdRegion);
                if (!allInRegions.Any(x => string.Equals(x.Name, shootingRange.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    _dalShootingRange.Create(shootingRange);
                }
            }
            else
            {
                res.IsOk = false;
                res.ErrorMessage = "Не найден пользователь";
            }

            return res;
        }

        /// <summary>
        /// Получить тир по идентификатору
        /// </summary>
        /// <param name="shootingRangeId">ид. стрелкового клуба</param>
        /// <returns></returns>
        public ShootingRangeParams Get(int shootingRangeId)
        {
            return _dalShootingRange.Get(shootingRangeId);
        }

        /// <summary>
        /// Получить список тиров по региону
        /// </summary>
        /// <param name="regionId">ид. региона</param>
        /// <returns></returns>
        public List<ShootingRangeListItem> GetByRegion(int? regionId)
        {
            return _dalShootingRange.GetByRegion(regionId);
        }
    }
}