using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Роль
    /// </summary>
    public class RoleParams
    {
        /// <summary>
        /// Ид. роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Кейчар
        /// </summary>
        public string Keychar { get; set; }

        public enum RoleEnum { 
            
            Organization = 1,
            Shooter = 2
        }
    }
}
