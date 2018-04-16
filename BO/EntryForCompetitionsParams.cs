using System;

namespace BO
{
    /// <summary>
    /// Заявка на упражнение определенного соревнования
    /// </summary>
    public class EntryForCompetitionsParams
    {
        /// <summary>
        /// Ид заявки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ид. стрелка
        /// </summary>
        public int IdShooter { get; set; }

        /// <summary>
        /// Ид. упражнения на соревновании
        /// </summary>
        public int IdCupCompetitionType { get; set; }

        /// <summary>
        /// Дата создания заявки
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Ид. статуса заявки
        /// </summary>
        public int IdEntryStatus { get; set; }
    }
}
