using System;

namespace BO
{
    /// <summary>
    /// Упражнение на соревновании
    /// </summary>
    public class CupCompetitionTypeParams : ModelBase
    {
        /// <summary>
        /// Идентификатор соревнования
        /// </summary>
        public int IdCup { get; set; }

        /// <summary>
        /// Идентификатор упражнения
        /// </summary>
        public int IdCompetitionType { get; set; }

        /// <summary>
        /// Время начала первой смены
        /// </summary>
        public DateTime? TimeFirstShift { get; set; }
    }
}
