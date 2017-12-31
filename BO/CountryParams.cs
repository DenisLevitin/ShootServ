using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Страна
    /// </summary>
    public class CountryParams
    {
        /// <summary>
        /// Ид. страны
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Код страны
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Название страны
        /// </summary>
        public string CountryName { get; set; }
    }
}
