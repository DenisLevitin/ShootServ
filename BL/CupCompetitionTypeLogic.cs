using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
