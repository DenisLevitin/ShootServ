using BO;
using System;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// DAL для таблицы USERS
    /// </summary>
    public class UserRepository : BaseRepository<UserParams, Users>
    {
        protected override UserParams ConvertToModel(Users entity)
        {
            return new UserParams
            {
                DateCreate = entity.DateCreate,
                FamilyName = entity.Family,
                FatherName = entity.FatherName,
                Id = entity.Id,
                IdRole = entity.IdRole,
                Login = entity.Login,
                Name = entity.Name,
                Password = entity.Password,
                Email = entity.E_mail
            };
        }

        protected override Users ConvertToEntity(UserParams model)
        {
            return new Users
            {
                DateCreate = model.DateCreate,
                Family = model.FamilyName,
                Name = model.Name,
                FatherName = model.FatherName,
                Id = model.Id,
                IdRole = model.IdRole,
                Login = model.Login,
                Password = model.Password,
                E_mail = model.Email
            };
        }

        protected override Func<Users, int> GetPrimaryKeyValue
        {
            get { return (x) => x.Id; }
        }

        /// <summary>
        /// Существует ли логин в базе
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsExistsLogin(string login)
        {
            using (var db = DBContext.GetContext())
            {
                return db.Users.Any(x => x.Login == login);
            }
        }

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login">логин пользователя</param>
        public UserParams GetByLogin(string login)
        {
            return GetFirstOrDefault(x => x.Login == login);
        }

        /// <summary>
        /// Получить пользователя по логину и паролю
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        public UserParams GetByLoginAndPassword(string login, string password)
        {
            return GetFirstOrDefault(x => x.Login == login && x.Password == password);
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="idShooter"></param>
        public UserParams GetByShooter(int idShooter)
        {
            return GetFirstOrDefault(x => x.Shooters.Any(y=>y.IdShooter == idShooter)); /// TODO: Сделать Shooter к User как один к одному
        }
    }
}