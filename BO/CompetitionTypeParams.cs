using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Стрелковое упражнение
    /// </summary>
    public class CompetitionTypeParams
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название упражнения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип оружия
        /// </summary>
        public int IdWeaponType { get; set; }

        /// <summary>
        /// Количество серий
        /// </summary>
        public int SeriesCount { get; set; }
    }
}
