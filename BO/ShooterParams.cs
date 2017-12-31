using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Класс стрелка
    /// </summary>
    public class ShooterParams
    {
        /// <summary>
        /// Ид. стрелка
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Имя 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// Ид. клуба
        /// </summary>
        public int IdClub { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Разряд
        /// </summary>
        public int IdCategory { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Тип оружия стрелка ( винтовка, пистолет )
        /// </summary>
        public int IdWeaponType { get; set; }

        /// <summary>
        /// Ид. пользователя
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Дата - время создания
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public int Sex { get; set; }

    }

    public enum SexEnum
    {
        Men = 1,
        Women = 0
    }
}
