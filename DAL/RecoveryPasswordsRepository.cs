using System;
using System.Linq;
using BO;

namespace DAL
{
    public class RecoveryPasswordsRepository
    {
        private RecoveryPasswordsParams Convert(RecoveredPasswords recPass)
        {
            return new RecoveryPasswordsParams
            {
                Id = recPass.Id,
                IdUser = recPass.IdUser,
                ActionDate = recPass.ActionDate,
                IsRecovered = recPass.IsRecovered,
                Password = recPass.Password
            };
        }

        private RecoveredPasswords Convert(RecoveryPasswordsParams recPass)
        {
            return new RecoveredPasswords
            {
                Id = recPass.Id,
                ActionDate = recPass.ActionDate,
                IdUser = recPass.IdUser,
                IsRecovered = recPass.IsRecovered,
                Password = recPass.Password
            };
        }

        /// <summary>
        /// Добавить запись в пул восстанавливаемых паролей
        /// </summary>
        /// <param name="recPass">восстанавливаемый пароль</param>
        /// <returns></returns>
        public ResultInfoStruct<int> Add(RecoveryPasswordsParams recPass)
        {
            var res = new ResultInfoStruct<int>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var newRecord = Convert(recPass);

                    db.RecoveredPasswords.Add(newRecord);
                    db.SaveChanges();

                    res.Data = newRecord.Id;
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Произошла ошибка при добавлении записи в RecoveryPasswords";
                res.Result.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Получить запись по идентификатору
        /// </summary>
        /// <param name="id">ид. записи</param>
        /// <returns></returns>
        public ResultInfoRef<RecoveryPasswordsParams> Get(int id)
        {
            var res = new ResultInfoRef<RecoveryPasswordsParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res.Data = Convert(db.RecoveredPasswords.Single(x => x.Id == id));
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "При получении записи из таблицы RecoveryPasswords произошла ошибка";
                res.Result.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Получить последний не восттановленный по пользователю
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <returns></returns>
        public ResultInfoRef<RecoveryPasswordsParams> GetLastNotRecoverdByUser(int idUser)
        {
            var res = new ResultInfoRef<RecoveryPasswordsParams>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var query = (from rec in db.RecoveredPasswords
                        where rec.IdUser == idUser && ! rec.IsRecovered
                        orderby rec.ActionDate descending
                        select rec).Take(1).Single();

                    res.Data = Convert(query);
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "При получении записи из таблицы RecoveryPasswords произошла ошибка";
                res.Result.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Обновить запист
        /// </summary>
        /// <param name="id">ид</param>
        /// <param name="recPass">пароль</param>
        /// <returns></returns>
        public ResultInfo Update(int id, RecoveryPasswordsParams recPass)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var updating = db.RecoveredPasswords.Where(x => x.Id == id).Single();

                    updating.ActionDate = recPass.ActionDate;
                    updating.IdUser = recPass.IdUser;
                    updating.IsRecovered = recPass.IsRecovered;
                    updating.Password = recPass.Password;

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.ErrorMessage = "При обновлении записи в таблице RecoveryPasswords произошла ошибка";
                res.Exc = exc;
            }

            return res;
        }

    }
}
