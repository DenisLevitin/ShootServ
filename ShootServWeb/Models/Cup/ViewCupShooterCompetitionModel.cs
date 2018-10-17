using System;

namespace ShootServ.Models.Cup
{
    /// <summary>
    /// Модель для детализации упражнения на странице просмотра соревнований
    /// </summary>
    public class ViewCupShooterCompetitionModel
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