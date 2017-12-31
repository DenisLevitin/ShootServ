using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// DAL для таблицы USERS
    /// </summary>
    public class EFUser
    {
        private Users Convert(UserParams user)
        {
            return new Users
            {
                DateCreate = user.DateCreate,
                Family = user.FamilyName,
                Name = user.Name,
                FatherName = user.FatherName,
                Id = user.Id,
                IdRole = user.IdRole,
                Login = user.Login,
                Password = user.Password,
                E_mail = user.Email
            };
        }

        private UserParams Convert(Users users)
        {
            return new UserParams
            {
                DateCreate = users.DateCreate,
                FamilyName = users.Family,
                FatherName = users.FatherName,
                Id = users.Id,
                IdRole = users.IdRole,
                Login = users.Login,
                Name = users.Name,
                Password = users.Password,
                Email = users.E_mail
            };
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user"></param>
        public ResultInfoStruct<int> Add(UserParams user)
        {
            var res = new ResultInfoStruct<int>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var adding = Convert(user);
                    db.Users.Add(adding);
                    db.SaveChanges();

                    res.Data = adding.Id;
                }
                catch (Exception exc)
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "Не удалось добавить пользователя в базу";
                    res.Result.Exc = exc;
                }
            }

            return res;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserParams Get(int idUser)
        {
            var res = new UserParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.Users.Where(x => x.Id == idUser).Single();
                    res = Convert(query);
                }
                catch (Exception exc)
                {
                    throw new Exception("Не удалось получить пользователя по идентификатору");
                }
            }

            return res;
        }

        /// <summary>
        /// Существует ли логин в базе
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public ResultInfoStruct<bool> IsExistsLogin(string login)
        {
            var res = new ResultInfoStruct<bool>();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    res.Data = db.Users.Where(x => x.Login == login).Any();
                }
            }
            catch (Exception exc)
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Не удалось получить пользователя по логину";
                res.Result.Exc = exc;
            }

            return res;
        }

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login">логин пользователя</param>
        public ResultInfoRef<UserParams> GetByLogin(string login)
        {
            var res = new ResultInfoRef<UserParams>();
            
                try
                {
                    using (var db = DBContext.GetContext())
                    {
                        var query = db.Users.Where(x => x.Login == login).Single();
                        res.Data = Convert(query);
                    }
                }
                catch (Exception exc)
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "Не удалось получить пользователя по логину";
                    res.Result.Exc = exc;
                }

            return res;
        }

        /// <summary>
        /// Получить пользователя по логину и паролю
        /// </summary>
        /// <param name="user"></param>
        public List<UserParams> GetByLoginAndPassword(string login, string password)
        {
            var res = new List<UserParams>();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = db.Users.Where(x => x.Login == login && x.Password == password).ToList();
                    res = query.ConvertAll(Convert);
                }
                catch (Exception exc)
                {
                    throw new Exception("Не удалось получить пользователя по логину и паролю");
                }
            }

            return res;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserParams GetByShooter(int idShooter)
        {
            var res = new UserParams();
            using (var db = DBContext.GetContext())
            {
                try
                {
                    var query = (from user in db.Users
                                 join shooter in db.Shooters on user.Id equals shooter.IdShooter
                                 where shooter.IdShooter == idShooter
                                 select user).Single();
                    res = Convert(query);
                }
                catch (Exception exc)
                {
                    throw new Exception("Не удалось получить пользователя по стрелку");
                }
            }

            return res;
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="idUser">ид. пользователя</param>
        /// <param name="user">пользователь</param>
        public ResultInfo Update(int idUser, UserParams user)
        {
            var res = new ResultInfo();

            try
            {
                using (var db = DBContext.GetContext())
                {
                    var updating = db.Users.Where(x => x.Id == idUser).Single();

                    updating.Family = user.FamilyName;
                    updating.FatherName = user.FatherName;
                    updating.Name = user.Name;
                    updating.Login = user.Login;
                    updating.Password = user.Password;
                    updating.IdRole = user.IdRole;
                    updating.E_mail = user.Email;

                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                res.IsOk = false;
                res.Exc = exc;
                res.ErrorMessage = "Произошла ошибка при обновлении пользователя";
            }

            return res;
        }
    }
}
