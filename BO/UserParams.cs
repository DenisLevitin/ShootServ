using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserParams
    {
        /// <summary>
        /// Ид. пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия стрелка
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int IdRole { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        public UserParams()
        {
            Login = "";
            Password = "";
            Name = "";
            FamilyName = "";
            FatherName = "";
            Email = "";
            IdRole = 1;
            DateCreate = new DateTime();
        }
    }

    /// <summary>
    /// Перечислитель ролей
    /// </summary>
    public enum RolesEnum 
    {
        Organization = 1,
        Shooter = 2
    }
}
