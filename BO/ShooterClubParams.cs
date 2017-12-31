using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Стрелковый клуб ( команда )
    /// </summary>
    public class ShooterClubParams
    {
        /// <summary>
        /// Ид. клуба
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название стрелкового клуба
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес стрелкового клуба
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Главный тренер
        /// </summary>
        public string MainCoach { get; set; }

        /// <summary>
        /// Ид. тира, к которому относится стрелковый клуб
        /// </summary>
        public int IdShootingRange { get; set; }

        /// <summary>
        /// Пользователь, создавший
        /// </summary>
        public int UsId { get; set; }

        /// <summary>
        /// Дата - время создания
        /// </summary>
        public DateTime DateCreate { get; set; }
    }
}
