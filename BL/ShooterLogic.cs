using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    /// <summary>
    /// БЛ для работы со стрелком
    /// </summary>
    public class ShooterLogic
    {
        private readonly ShooterRepository _dalShooter;
        private readonly WeaponTypeRepository _weaponTypeRepository;
        private static IReadOnlyCollection<WeaponTypeParams> _weaponTypes;

        public ShooterLogic()
        {
            _dalShooter = new ShooterRepository();
            _weaponTypeRepository = new WeaponTypeRepository();
        }

        /// <summary>
        /// Добавить стрелка
        /// </summary>
        /// <param name="shooter"></param>
        /// <returns></returns>
        public ResultInfoStruct<int> Add(ShooterParams shooter)
        {
            var res = new ResultInfoStruct<int>();
            if (!String.IsNullOrEmpty(shooter.Name) && !string.IsNullOrEmpty(shooter.Family))
            {
                shooter.DateCreate = DateTime.Now;
                res.Data = _dalShooter.Create(shooter);
            }
            else 
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Фамилия и имя стрелка не могут быть пустыми";
            }

            return res;
        }

        /// <summary>
        /// Получить стрелка по Id
        /// </summary>
        /// <returns></returns>
        public ShooterParams GetByUser(int idUser)
        {
            return _dalShooter.GetByUser(idUser);
        }

        /// <summary>
        /// Получить список стрелков по клубу
        /// </summary>
        /// <param name="idClub"></param>
        /// <returns></returns>
        public List<ShooterParams> GetByClub(int idClub)
        {
            return _dalShooter.GetByClub(idClub);
        }

        /// <summary>
        /// Получить список стрелков, заявленных на соревнование
        /// </summary>
        /// <param name="cupId">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterParams> GetShootersWasEntryOnCup(int cupId)
        {
            return _dalShooter.GetShootersWasEntryOnCup(cupId);
        }

        /// <summary>
        /// Получить список стрелков, заявленных на соревнование от определенного клуба
        /// </summary>
        /// <param name="cupId">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterParams> GetShootersWasEntryOnCupInClub(int cupId, int clubId)
        {
            return _dalShooter.GetShootersWasEntryOnCupInClub(cupId, clubId);
        }

        /// <summary>
        /// Получить заявленных стрелков на соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<ShooterEntryDetailsParams> GetEntryShootersOnCup(int idCup, bool isFilterBySex = false, SexEnum sex = SexEnum.Men)
        {
            var res = _dalShooter.GetEntryShootersOnCup(idCup);
            if (isFilterBySex)
            {
                res = res.Where(x => x.SexEnum == sex).ToList();
            }

            return res;
        }

        /// <summary>
        /// Получить заявленных стрелков на соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idClub">ид. команды</param>
        /// <returns></returns>
        public List<ShooterEntryDetailsParams> GetEntryShootersOnCupAndClub(int idCup, int idClub, bool isFilterBySex = false, SexEnum sex = SexEnum.Men)
        {
            var res = _dalShooter.GetEntryShootersOnCupAndClub(idCup, idClub);
            if (isFilterBySex)
            {
                res = res.Where(x => x.SexEnum == sex).ToList();
            }

            return res;
        }

        /// <summary>
        /// Обновить стрелка
        /// </summary>
        /// <param name="idShooter">ид. стрелка</param>
        /// <param name="shooter">стрелок</param>
        /// <returns></returns>
        public void Update(int idShooter, ShooterParams shooter)
        {
            _dalShooter.Update(shooter, idShooter);
        }

        public IReadOnlyCollection<WeaponTypeParams> GetAllWeaponTypes()
        {
            if ( _weaponTypes == null)
            {
                _weaponTypes = _weaponTypeRepository.GetAll();
            }

            return _weaponTypes;
        }
    }
}
