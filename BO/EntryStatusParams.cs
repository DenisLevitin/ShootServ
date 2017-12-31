using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Статус заявки
    /// </summary>
    public class EntryStatusParams
    {
        /// <summary>
        /// Ид. статуса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// кейчар
        /// </summary>
        public string KeyChar { get; set; }

        /// <summary>
        /// Перечислитель статусов заявок
        /// </summary>
        public enum EntryStatusEnum
        {
            /// <summary>
            /// Принята
            /// </summary>
            Accepted = 1,

            /// <summary>
            /// Отклонена
            /// </summary>
            Denied = 2,

            /// <summary>
            /// Ожидает рассмотрения
            /// </summary>
            Waiting = 3
        }
    }
}
