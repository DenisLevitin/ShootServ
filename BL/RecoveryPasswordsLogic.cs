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
        public ResultInfoStruct<int> QueryForRecoveryPassword(int idUser, string newHashPassword)
        {
            var newRecPass = new RecoveryPasswordsParams
            {
                IdUser = idUser,
                Password = newHashPassword,
                ActionDate = DateTime.Now,
                IsRecovered = false
            };

            return _dalRecoveryPasswords.Add(newRecPass);
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

            var queryLastRec = _dalRecoveryPasswords.Get(idRec);
            if (queryLastRec.Result.IsOk)
            {
                if (!queryLastRec.Data.IsRecovered) // проверяем, что пароль не был ранее востановлен по данной ссылке
                {
                    queryLastRec.Data.ActionDate = DateTime.Now;
                    queryLastRec.Data.IsRecovered = true;

                    var userLogic = new UserLogic();
                    var getUser = userLogic.Get(idUser);

                    if (getUser != null)
                        /// TODO: Привести результат вызова в божеский вид, чтоб не обрабатывать исключения
                    {
                        using (var tran = new TransactionScope())
                        {
                            var queryUpdate = _dalRecoveryPasswords.Update(queryLastRec.Data.Id, queryLastRec.Data);
                            // Обновляем статус в таблице восстанавливаемых паролей
                            if (queryUpdate.IsOk)
                            {
                                getUser.Password = queryLastRec.Data.Password;
                                res = userLogic.Update(idUser, getUser, false); // обновляем пароль в таблице Users

                                if (res.IsOk)
                                {
                                    tran.Complete();
                                }
                            }
                            else res = queryUpdate;
                        }
                    }
                }
                else
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Пароль был ранее активирован. При возникновении проблем с авторизацией попробуйте восстановить пароль еще раз";
                }
            }
            else res = queryLastRec.Result;

            return res;
        }

    }
}
