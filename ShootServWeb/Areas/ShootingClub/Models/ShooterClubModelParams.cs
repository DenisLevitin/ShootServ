using System.ComponentModel;

namespace ShootServ.Areas.ShootingClub.Models
{
    /// <summary>
    /// Модель стрелкового клуба для добавления
    /// </summary>
    public class ShooterClubModelParams
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название стрелкового клуба
        /// </summary>
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        /// Адрес стрелкового клуба
        /// </summary>
        [DisplayName("Адрес")]
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        /// <summary>
        /// Ид. тира
        /// </summary>
        [DisplayName("Тир")]
        public int IdShootingRange { get; set; }

        /// <summary>
        /// Главный тренер
        /// </summary>
        [DisplayName("Главный тренер")]
        public string MainCoach { get; set; }
    }
}