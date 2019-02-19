using System;
using System.Transactions;
using BO;
using DAL;

namespace BL
{
    /// <summary>
    /// Бизнес логика работы с восстанавливаемыми паролями
    /// </summary>
    public class RecoveryPasswordsLogic
    {
        private readonly RecoveryPasswordsRepository _dalRecoveryPasswords;

        public RecoveryPasswordsLogic()
        {
            _dalRecoveryPasswords = new RecoveryPasswordsRepository();
        }

        /// <summary>
        /// Запрос на восстановление пароля
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <param name="newHashPassword">новый хэш пароль</param>
        /// <returns></returns>
        public int QueryForRecoveryPassword(int idUser, string newHashPassword)
        {
            var newRecPass = new RecoveryPasswordsParams
            {
                IdUser = idUser,
                Password = newHashPassword,
                ActionDate = DateTime.Now,
                IsRecovered = false
            };

            return _dalRecoveryPasswords.Create(newRecPass);
        }

        /// <summary>
        /// Восстановить пароль для пользователя
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <param name="idRec">ид. записи в таблице RecoveredPasswords</param>
        /// <returns></returns>
        public ResultInfo RecoveryPassword(int idUser, int idRec)
        {
            var res = new ResultInfo();

            var recoveryPassword = _dalRecoveryPasswords.Get(idRec);
            if (!recoveryPassword.IsRecovered) // проверяем, что пароль не был ранее востановлен по данной ссылке
            {
                recoveryPassword.ActionDate = DateTime.Now;
                recoveryPassword.IsRecovered = true;

                var userLogic = new UserLogic();
                var getUser = userLogic.Get(idUser);

                if (getUser != null)
                {
                    using (var tran = new TransactionScope())
                    {
                        _dalRecoveryPasswords.Update(recoveryPassword, recoveryPassword.Id);

                        // Обновляем статус в таблице восстанавливаемых паролей
                        getUser.Password = recoveryPassword.Password;
                        userLogic.Update(idUser, getUser, false); // обновляем пароль в таблице Users

                        tran.Complete();
                    }
                }
            }
            else
            {
                res.IsOk = false;
                res.ErrorMessage = "Пароль был ранее активирован. При возникновении проблем с авторизацией попробуйте восстановить пароль еще раз";
            }

            return res;
        }
    }
}