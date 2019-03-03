using System;

namespace BO
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserParams : ModelBase
    {
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

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Name, FamilyName);
            }
        }

        public UserParams()
        {
            IdRole = 1;
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
