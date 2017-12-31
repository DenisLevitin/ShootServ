using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Детализация заявки стрелка
    /// </summary>
    public class ShooterEntryDetailsParams
    {
        /// <summary>
        /// Ид. стрелка
        /// </summary>
        public int IdShooter { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public string Sex
        {
            get
            {
                return SexEnum == SexEnum.Men ?  "мужской" : "женский";
            }
        }

        /// <summary>
        /// Пол
        /// </summary>
        public SexEnum SexEnum { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Разряд
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Название команды
        /// </summary>
        public string ClubName { get; set; }

        /// <summary>
        /// Список упражнений
        /// </summary>
        public List<string> Competitions { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Название тира
        /// </summary>
        public string ShootRangeName { get; set; }

        /// TODO: Возможно потребуется добавить список Id заявок
    }
}
