using System;
using BO;
using DAL;

namespace BL
{
    public class ResultLogic
    {
        private readonly ResultRepository _resultRepository;

        private readonly CupCompetitionTypeLogic _cupCompetitionTypeLogic;

        private readonly CupLogic _cupLogic;

        public ResultLogic()
        {
            _resultRepository = new ResultRepository();
            _cupCompetitionTypeLogic = new CupCompetitionTypeLogic();
            _cupLogic = new CupLogic();
        }

        public ResultInfo AddNewResult(int idShooter, int idCompetitionCupType, int idCurrentUser, float[] series)
        {
            var cupCompetitionType = _cupCompetitionTypeLogic.Get(idCompetitionCupType);

            if (cupCompetitionType == null)
            {
                throw new Exception(""); /// something like ObjectNotFoundException
            }

            // проверить, кто добавляет результат
            var cup = _cupLogic.Get(cupCompetitionType.IdCup);
            if (cup.IdUser == idCurrentUser)
            {


                // проверить кол-во серий
                _resultRepository.Create();
            }
            else
            {
                return new ResultInfo() {IsOk = false, ErrorMessage = "Только пользователь, создавший соревнование может добавить результат"};
            }
        }
    }
}
