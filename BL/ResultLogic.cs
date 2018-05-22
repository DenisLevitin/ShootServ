using System;
using System.Collections.Generic;
using BO;
using BO.Exceptions;
using DAL;

namespace BL
{
    public class ResultLogic
    {
        private readonly ResultRepository _resultRepository;

        private readonly CupCompetitionTypeLogic _cupCompetitionTypeLogic;

        private readonly CompetitionTypeLogic _competitionTypeLogic;

        private readonly CupLogic _cupLogic;

        public ResultLogic()
        {
            _resultRepository = new ResultRepository();
            _cupCompetitionTypeLogic = new CupCompetitionTypeLogic();
            _competitionTypeLogic = new CompetitionTypeLogic();
            _cupLogic = new CupLogic();
        }

        private void AddSeriesToResult(float[] series, ResultModelParams result)
        {
            if ( series.Length > 0)
            {
                result.Serie1 = series[0];
            }

            if (series.Length > 1)
            {
                result.Serie2 = series[1];
            }

            if (series.Length > 2)
            {
                result.Serie3 = series[2];
            }

            if (series.Length > 3)
            {
                result.Serie4 = series[3];
            }

            if (series.Length > 4)
            {
                result.Serie5 = series[4];
            }

            if (series.Length > 5)
            {
                result.Serie6 = series[5];
            }
        }

        /// <summary>
        /// Добавить результат стрелка на соревновании
        /// </summary>
        /// <param name="idShooter"></param>
        /// <param name="idCompetitionCupType">сквозной ид. упражнения на соревновании</param>
        /// <param name="idCurrentUser"></param>
        /// <param name="series"></param>
        /// <returns></returns>
        public ResultInfoStruct<int> AddNewResult(int idShooter, int idCompetitionCupType, int idCurrentUser, float[] series)
        {
            var res = new ResultInfoStruct<int>();
            var cupCompetitionType = _cupCompetitionTypeLogic.Get(idCompetitionCupType);

            if (cupCompetitionType == null)
            {
                throw new ObjectNotFoundException($"CupCompetitionType with id {idCompetitionCupType}");
            }

            // проверить, кто добавляет результат
            var cup = _cupLogic.Get(cupCompetitionType.IdCup);
            if (cup.IdUser == idCurrentUser)
            {
                // проверить кол-во серий
                var competitionType = _competitionTypeLogic.Get(cupCompetitionType.IdCompetitionType);
                if ( series.Length == competitionType.SeriesCount)
                {
                    var competitionResult = new ResultModelParams
                    {
                        IdShooter = idShooter,
                        IdCompetitionTypeCup = idCompetitionCupType,
                    };
                    AddSeriesToResult(series, competitionResult);
                    res.Data = _resultRepository.Create(competitionResult); /// TODO: Здесь по нормальному сделать надо, заполнение объекта с сериями сейчас некорректное
                }
                else
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "Нельзя добавить результат, т.к количество серий не совпадат с тем, которое должно быть в упражнении";
                }
            }
            else
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Только пользователь, создавший соревнование может добавить результат";
            }

            return res;
        }

        /// TODO: Возможно ниже придется альтернативу методу по IdCup и IdCompetitionType

        /// <summary>
        /// Получить результат соревнований в упражнении
        /// </summary>
        /// <param name="idCompetitionTypeCup"></param>
        /// <returns></returns>
        public List<ResultModelParams> GetByCompetitionInCup(int idCompetitionTypeCup)
        {
            return _resultRepository.GetByCompetitionInCup(idCompetitionTypeCup);
        }
    }
}
