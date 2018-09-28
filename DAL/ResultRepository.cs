using System;
using System.Collections.Generic;
using System.Linq;
using BO;

namespace DAL
{
    public class ResultRepository : BaseRepository<ResultModelParams, Results>
    {
        protected override Func<Results, int> GetPrimaryKeyValue
        {
            get { return (x) => x.Id; }
        }

        protected override Results ConvertToEntity(ResultModelParams model)
        {
            return new Results()
            {
                Id = model.Id,
                IdShooter = model.IdShooter,
                IdCompetitionTypeCup = model.IdCompetitionTypeCup,
                ResultOfFinal = model.ResultOfFinal,
                Serie1 = model.Serie1,
                Serie2 = model.Serie2,
                Serie3 = model.Serie3,
                Serie4 = model.Serie4,
                Serie5 = model.Serie5,
                Serie6 = model.Serie6
            };
        }

        protected override ResultModelParams ConvertToModel(Results entity)
        {
            return new ResultModelParams()
            {
                Id = entity.Id,
                IdShooter = entity.IdShooter,
                IdCompetitionTypeCup = entity.IdCompetitionTypeCup,
                ResultOfFinal = entity.ResultOfFinal,
                Serie1 = entity.Serie1,
                Serie2 = entity.Serie2,
                Serie3 = entity.Serie3,
                Serie4 = entity.Serie4,
                Serie5 = entity.Serie5,
                Serie6 = entity.Serie6
            };
        }

        /// <summary>
        /// Получить результат соревнования стрелка в конкретном соревновании
        /// </summary>
        /// <param name="idShooter">ид. стрелка</param>
        /// <param name="idCompetitionTypeCup">ид. упражнения на соревновании</param>
        /// <returns></returns>
        public ResultModelParams GetByShooterAndCup(int idShooter, int idCompetitionTypeCup)
        {
            return GetFiltered(x => x.IdShooter == idShooter && x.IdCompetitionTypeCup == idCompetitionTypeCup).FirstOrDefault();
        }

        /// <summary>
        /// Получить результаты упражнения на соревновании
        /// </summary>
        /// <param name="idCompetitionTypeCup">ид. соревнования</param>
        /// <returns></returns>
        public List<ResultModelParams> GetByCompetitionInCup(int idCompetitionTypeCup)
        {
            return GetFiltered(x => x.IdCompetitionTypeCup == idCompetitionTypeCup)
                .OrderByDescending(x => x.Serie1.GetValueOrDefault() + x.Serie2.GetValueOrDefault() + x.Serie3.GetValueOrDefault() + x.Serie4.GetValueOrDefault() + x.Serie5.GetValueOrDefault() + x.Serie6.GetValueOrDefault()).ToList();
        }
    }
}
