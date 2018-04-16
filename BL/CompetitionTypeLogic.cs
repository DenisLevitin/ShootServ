using BO;
using DAL;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// БЛ для упражения
    /// </summary>
    public class CompetitionTypeLogic
    {
        private readonly EFCompetitionType _dalCompetitionType;

        public CompetitionTypeLogic()
        {
            _dalCompetitionType = new EFCompetitionType();
        }

        /// <summary>
        /// Получить упражнение по идентификатору
        /// </summary>
        /// <param name="id">ид</param>
        /// <returns></returns>
        public CompetitionTypeParams Get(int id)
        {
            return _dalCompetitionType.Get(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CompetitionTypeParams> GetAll()
        {
            return _dalCompetitionType.GetAll();
        }

        /// <summary>
        /// Получить упражнения по типу оружия
        /// </summary>
        /// <param name="idWeapontType"></param>
        /// <returns></returns>
        public List<CompetitionTypeParams> GetByWeaponType(int idWeapontType)
        {
            return _dalCompetitionType.GetByWeaponType(idWeapontType);
        }

        /// <summary>
        /// Получить все упражнения по соревнованию
        /// </summary>
        /// <returns></returns>
        public List<CompetitionTypeParams> GetByCup(int idCup)
        {
            return _dalCompetitionType.GetByCup(idCup);
        }

        /// <summary>
        /// Получить детализацию упражнений на соревновании с состоянием о заявке стрелков
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <param name="idUser">ид. пользователя</param>
        /// <returns></returns>
        public List<CupShooterCompetitionParams> GetCupCompetitionListWithShooterEntryDetails(int idCup, int idUser=-1)
        {
            return _dalCompetitionType.GetCupCompetitionListWithShooterEntryDetails(idCup, idUser);
        }
    }
}
