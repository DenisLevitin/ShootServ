using System;

namespace BO
{
    /// <summary>
    /// Детализация упражнения на соревновании для стрелка
    /// </summary>
    public class CupShooterCompetitionParams
    {
        /// <summary>
        /// Ид. упражнения на соревновании
        /// </summary>
        public int IdCupCompetitionType { get; set; }

        /// <summary>
        /// Ид. типа упражнения
        /// </summary>
        public int IdCompetitionType { get; set; }

        /// <summary>
        /// Название упражнения
        /// </summary>
        public string NameCompetition { get; set; }

        /// <summary>
        /// Время первой смены
        /// </summary>
        public DateTime TimeFirstShift { get; set; }

        /// <summary>
        /// Был ли стрелок заявлен на упражнение
        /// </summary>
        public bool IsShooterWasEntried { get; set; }
    }
}
