using BO;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class CupCompetitionTypeLogic
    {
        private readonly EFCupCompetitionType _dalCupCompetitionType;

        public CupCompetitionTypeLogic()
        {
            _dalCupCompetitionType = new EFCupCompetitionType();
        }

        /// <summary>
        /// Получить список соревнований на соревновании
        /// </summary>
        /// <param name="idCup">ид. соревнования</param>
        /// <returns></returns>
        public List<CupCompetitionTypeParams> GetByCup(int idCup)
        {
            return _dalCupCompetitionType.GetByCup(idCup);
        }
    }
}
