using System.Collections.Generic;

namespace ShootServ.Models.Cup
{
    /// <summary>
    /// Модель для просмотра упражнений на странице просмотра соревнований
    /// </summary>
    public class ViewCupCompetitionModel
    {
        /// <summary>
        /// Список упражнений
        /// </summary>
        public List<ViewCupShooterCompetitionModel> Competitions { get; set; }

        /// <summary>
        /// Показывать ли кнопку добавления на соревнования
        /// </summary>
        public bool ShowEntryButton { get; set; }

        /// <summary>
        /// Ид. соревнования
        /// </summary>
        public int IdCup { get; set; }
    }
}