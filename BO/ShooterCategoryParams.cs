using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Разряд стрелка
    /// </summary>
    public class ShooterCategoryParams
    {
        /// <summary>
        /// Ид. разряда
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название разряда
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Порядковый номер разряда
        /// </summary>
        public int OrderSort { get; set; }
    }
}
