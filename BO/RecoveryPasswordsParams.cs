using System;

namespace BO
{
    /// <summary>
    /// Запись из пула паролей для восстановления
    /// </summary>
    public class RecoveryPasswordsParams : ModelBase
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Ид. пользователя
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Восстановлен пароль или нет
        /// </summary>
        public bool IsRecovered { get; set; }

        /// <summary>
        /// Дата последнего действия в записи
        /// </summary>
        public DateTime ActionDate { get; set; }

        public RecoveryPasswordsParams()
        {
            IsRecovered = false;
        }
    }
}
