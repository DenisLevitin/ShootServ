using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// БЛ, для работы со стрелковыми клубами
    /// </summary>
    public class ShootingClubLogic
    {
        private readonly EFShooterClub _dalShootingClubLogic;
        private readonly UserLogic _userLogic;
        private readonly ShooterLogic _shooterLogic;

        public ShootingClubLogic()
        {
            _dalShootingClubLogic = new EFShooterClub();
            _userLogic = new UserLogic();
            _shooterLogic = new ShooterLogic();
        }

        /// <summary>
        /// Добавить стрелковый клую
        /// </summary>
        /// <param name="shooterClub"></param>
        /// <returns></returns>
        public ResultInfo Add(ShooterClubParams shooterClub)
        {
            var res = new ResultInfo();

            if (!string.IsNullOrEmpty(shooterClub.Name))
            {

                var listExists = _dalShootingClubLogic.GetByName(shooterClub.Name);
                if (!listExists.Any())
                {

                    shooterClub.DateCreate = DateTime.Now;

                    var queryUser = _userLogic.Get(shooterClub.UsId);

                    if (queryUser != null)
                    {
                        if (queryUser.IdRole == (int)RolesEnum.Organization)
                        {
                            res = _dalShootingClubLogic.Add(shooterClub);
                        }
                        else {
                            res.IsOk = false;
                            res.ErrorMessage = "Пользователь не является организатором";
                        }
                    }
                    else
                    {
                        res.IsOk = false;
                        res.ErrorMessage = "Не найден пользователь";
                    }
                }
                else
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Стрелковый клуб с таким названием уже существует";
                }
            }
            else {
                res.IsOk = false;
                res.ErrorMessage = "Нельзя добавить тир с пустым названием";
            }

            return res;
        }

        /// <summary>
        /// Получить стрелковый клуб по Id
        /// </summary>
        /// <param name="clubId">ид. клуба</param>
        /// <returns></returns>
        public ShooterClubParams Get(int clubId)
        {
            return _dalShootingClubLogic.Get(clubId);
        }

        /// <summary>
        /// Получить список стрелковых клубов по региону
        /// </summary>
        /// <param name="idCountry">ид. страны</param>
        /// <param name="idRegion">ид. региона</param>
        /// <returns></returns>
        public List<ShooterClubDetalisationParams> GetByRegion(int idCountry =-1, int idRegion =-1)
        {
            return idRegion > 0 ? _dalShootingClubLogic.GetByRegion(idRegion) 
                        : idCountry > 0 ? 
                        _dalShootingClubLogic.GetByCountry(idCountry) : _dalShootingClubLogic.GetAll();
        }

        /// <summary>
        /// Обновить стрелковый клуб
        /// </summary>
        /// <param name="idClub"></param>
        /// <param name="shooterClub"></param>
        public void Update(int idClub, ShooterClubParams shooterClub)
        {
            _dalShootingClubLogic.Update(idClub, shooterClub);
        }

        /// <summary>
        /// Получить все клубы, учавствующие в соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByCup(int idCup)
        {
            return _dalShootingClubLogic.GetByCup(idCup);
        }

        /// <summary>
        /// Получить список стрелковых клубов по ид тира
        /// </summary>
        /// <param name="idShootnigRange">ид. тира</param>
        /// <returns></returns>
        public List<ShooterClubParams> GetByShootingRange(int idShootnigRange)
        {
            return _dalShootingClubLogic.GetByShootingRange(idShootnigRange);
        }

        /// <summary>
        /// Удалить стрелковый клуб
        /// </summary>
        /// <param name="idClub"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public ResultInfo Delete(int idClub, int idUser)
        {
            var res = new ResultInfo();

            var shooters = _shooterLogic.GetByClub(idClub);
            if (!shooters.Any())
            {
                var club = _dalShootingClubLogic.Get(idClub);
                if (club.UsId == idUser)
                {
                    res = _dalShootingClubLogic.Delete(idClub);
                }
                else 
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Нельзя удалить стрелковый клуб, т.к пользователь его не добавлял";
                }
            }
            else 
            {
                res.IsOk = false;
                res.ErrorMessage = "Нельзя удалить стрелковый клуб, т.к к нему привязаны стрелки";
            }

            return res;
        }
    }
}
