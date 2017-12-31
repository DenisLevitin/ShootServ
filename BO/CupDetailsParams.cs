using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Детализация соревнования
    /// </summary>
    public class CupDetailsParams
    {
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DisplayName("Название")]
        public string Name { get; set; }

        /// <summary>
        /// Название тира
        /// </summary>
        [DisplayName("Тир")]
        public string RangeName { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [DisplayName("Город")]
        public string Town { get; set; }

        /// <summary>
        /// Адрес тира
        /// </summary>
        [DisplayName("Адрес тира")]
        public string RangeAddress { get; set; }

        /// <summary>
        /// Телефон тира
        /// </summary>
        [DisplayName("Телефон тира")]
        public string RangePhone { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        [DisplayName("Регион")]
        public string Region { get; set; }

        /// <summary>
        /// Масштаб соревнования
        /// </summary>
        [DisplayName("Масштаб соревнования")]
        public string CupType { get; set; }

        /// <summary>
        /// Дата начала соревнования
        /// </summary>
        [DisplayName("Дата начала соревнования")]
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Дата окончания соревнования
        /// </summary>
        [DisplayName("Дата окончания соревнования")]
        public DateTime DateEnd { get; set; }
    }
}
