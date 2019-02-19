using System;
using System.Linq;
using BO;

namespace DAL
{
    public class RecoveryPasswordsRepository : BaseRepository<RecoveryPasswordsParams, RecoveredPasswords>
    {
        protected override Func<RecoveredPasswords, int> GetPrimaryKeyValue
        {
            get { return (x) => x.Id; }
        }

        protected override RecoveryPasswordsParams ConvertToModel(RecoveredPasswords entity)
        {
            return new RecoveryPasswordsParams
            {
                Id = entity.Id,
                IdUser = entity.IdUser,
                ActionDate = entity.ActionDate,
                IsRecovered = entity.IsRecovered,
                Password = entity.Password
            };
        }

        protected override RecoveredPasswords ConvertToEntity(RecoveryPasswordsParams model)
        {
            return new RecoveredPasswords
            {
                Id = model.Id,
                ActionDate = model.ActionDate,
                IdUser = model.IdUser,
                IsRecovered = model.IsRecovered,
                Password = model.Password
            };
        }

        /// <summary>
        /// Получить последний не восттановленный по пользователю
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <returns></returns>
        public ResultInfoRef<RecoveryPasswordsParams> GetLastNotRecoverdByUser(int idUser)
        {
            var res = new ResultInfoRef<RecoveryPasswordsParams>();

            using (var db = DBContext.GetContext())
            {
                var query = (from rec in db.RecoveredPasswords
                    where rec.IdUser == idUser && !rec.IsRecovered
                    orderby rec.ActionDate descending
                    select rec).Take(1).FirstOrDefault();

                res.Data = ConvertToModel(query);
            }

            return res;
        }
    }
}