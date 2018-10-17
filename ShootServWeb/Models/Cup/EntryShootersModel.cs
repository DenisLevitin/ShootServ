using System.Collections.Generic;
using System.Web.Mvc;
using BO;

namespace ShootServ.Models.Cup
{
    /// <summary>
    /// Модель для отображения завляенных стрелков на соревновании
    /// </summary>
    public class EntryShootersModel
    {
        /// <summary>
        /// Список стрелков
        /// </summary>
        public List<ShooterEntryDetailsParams> Shooters { get; set; }

        /// <summary>
        /// Список стрелковых команд
        /// </summary>
        public List<SelectListItem> Clubs { get; set; }
             
    }
}