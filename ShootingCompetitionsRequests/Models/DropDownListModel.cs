using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Models
{
    /// <summary>
    /// Модель, для выпадающего списка
    /// </summary>
    public class DropDownListModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Элементы списка
        /// </summary>
        public List<SelectListItem> Items { get; set; }
    }
}