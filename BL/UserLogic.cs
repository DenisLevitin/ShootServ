using BO;
using DAL;
using System;
using System.Text;
using System.Transactions;
using System.Security.Cryptography;

namespace BL
{
    /// <summary>
    /// БЛ по пользователю
    /// </summary>
    public class UserLogic
    {
        private readonly UserRepository _dalUser;
        private readonly ShooterLogic _blShooter;
        private readonly RecoveryPasswordsLogic _recoveryPasswordsLogic;

        public UserLogic()
        {
            _dalUser = new UserRepository();
            _blShooter = new ShooterLogic();
            _recoveryPasswordsLogic = new RecoveryPasswordsLogic();
        }

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public UserParams GetByLogin(string login)
        {
            return _dalUser.GetByLogin(login);
        }

        /// <summary>
        /// Проверка на существование логина
        /// </summary>
        /// <param name="login">логин</param>
        /// <returns></returns>
        public bool IsExistsLogin(string login)
        {
            return _dalUser.IsExistsLogin(login);
        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <returns></returns>
        public ResultInfoRef<UserParams> Authentification(string login, string password)
        {
            var res = new ResultInfoRef<UserParams>() {Data = null};

            password = HashPassword(password);
            var user = _dalUser.GetByLoginAndPassword(login, password);
            if (user != null)
            {
                res.Data = user;
            }
            else
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Пользователь с таким паролем и логином не найден";
            }

            return res;
        }

        /// <summary>
        /// Захешировать пароль
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            var bytes = Encoding.ASCII.GetBytes(password);
            var md5 = MD5.Create();
            password = ASCIIEncoding.ASCII.GetString(md5.ComputeHash(bytes));

            return password;
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns></returns>
        private ResultInfoStruct<int> AddUserInternal(UserParams user)
        {
            var res = new ResultInfoStruct<int>();
            user.DateCreate = DateTime.Now;

            string noHashPassword = user.Password;
            if (!string.IsNullOrWhiteSpace(user.Login))
            {
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    if (user.Password.Length >= 7)
                    {
                        if (!string.IsNullOrWhiteSpace(user.FamilyName) && !string.IsNullOrWhiteSpace(user.Name))
                        {
                            user.Password = HashPassword(user.Password);

                            var queryExistLogin = IsExistsLogin(user.Login);
                            if (!queryExistLogin)
                            {
                                res.Data = _dalUser.Create(user);
                                
                                // Посылаем e-mail счастливому пользователю
                                string body = $@"Уважаемый пользователь, вы зарегистрировались на сервисе www.shoot-serv.ru
                                                 Через него вы можете подавать заявки на соревнования, либо создавать их и отслеживать список заявленных. 
                                                 Ваш логин {user.Login}, пароль {noHashPassword} ";
                                EmailSender.EmailHelper.SendMail(user.Email, "Регистрация на shoot-serv", body);
                            }
                            else
                            {
                                res.Result.IsOk = false;
                                res.Result.ErrorMessage = "Такой логин уже существует";
                            }
                        }
                        else
                        {
                            res.Result.IsOk = false;
                            res.Result.ErrorMessage = "Фамилия и имя пользователя не могут быть пустыми";
                        }
                    }
                    else
                    {
                        res.Result.IsOk = false;
                        res.Result.ErrorMessage = "Пароль не может быть меньше 7 символов";
                    }
                }
                else
                {
                    res.Result.IsOk = false;
                    res.Result.ErrorMessage = "Пароль не может быть пустым";
                }
            }
            else
            {
                res.Result.IsOk = false;
                res.Result.ErrorMessage = "Логин не может быть пустым";
            }

            return res;
        }

        /// <summary>
        /// Добавить стрелка
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="shooter">стрелок</param>
        /// <returns>ид. добавленного usera</returns>
        public ResultInfoStruct<int> AddShooter(UserParams user, ShooterParams shooter)
        {
            if (user.IdRole != (int) RolesEnum.Shooter)
            {
                throw new Exception("Некорректно указан роль пользователя");
            }

            var res = new ResultInfoStruct<int>();

            shooter.Name = user.Name;
            shooter.Family = user.FamilyName;
            shooter.FatherName = user.FatherName;
            user.IdRole = (int) RolesEnum.Shooter;

            using (var tran = new TransactionScope())
            {
                var queryAddUser = AddUserInternal(user);
                if (queryAddUser.Result.IsOk)
                {
                    res.Data = queryAddUser.Data;
                    shooter.IdUser = queryAddUser.Data;
                    res.Result = _blShooter.Add(shooter);

                    if (res.Result.IsOk)
                        tran.Complete();
                }
                else res.Result = queryAddUser.Result;
            }

            return res;
        }

        /// <summary>
        /// Добавить организатора
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultInfoStruct<int> AddOrganizator(UserParams user)
        {
            if (user.IdRole != (int) RolesEnum.Organization)
            {
                throw new Exception("Некорректно указан роль пользователя");
            }

            return AddUserInternal(user);
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public UserParams Get(int idUser)
        {
            return _dalUser.Get(idUser);
        }

        /// <summary>
        /// Получить пользователя по стрелку
        /// </summary>
        /// <param name="idShooter"></param>
        /// <returns></returns>
        public UserParams GetByShooter(int idShooter)
        {
            return _dalUser.GetByShooter(idShooter);
        }

        /// <summary>
        /// Обновить пользователя c хэшированием пароля
        /// </summary>
        /// <param name="idUser">ид пользователя</param>
        /// <param name="user">пользователь</param>
        /// <param name="needUpdatePassword">требуется ли обновить пароль</param>
        /// <returns></returns>
        public void Update(int idUser, UserParams user, bool needUpdatePassword)
        {
            if (needUpdatePassword)
            {
                user.Password = HashPassword(user.Password);
            }
            else
            {
                var currentUser = Get(idUser); // текущий существующий пользователь
                user.Password = currentUser.Password;
            }

            _dalUser.Update(user, idUser);
        }

        /// <summary>
        /// Запрос на восстановление пароля
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="email"></param>
        /// <returns></returns>
        public ResultInfo QueryForRecoveryPassword(string login, string email)
        {
            var res = new ResultInfo();

            var user = _dalUser.GetByLogin(login);
            if (user != null)
            {
                if (string.Equals(user.Email, email, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Создаем новый пароль от random методом
                    string newPassword = PasswordHelper.GetRandomPassword();

                    // Сохраняем новый пароль в таблицу неактивированных паролей
                    var queryRecAdd = _recoveryPasswordsLogic.QueryForRecoveryPassword(user.Id, HashPassword(newPassword));
                    if (queryRecAdd.Result.IsOk)
                    {
                        // неактивированный пароль отправляем на e-mail пользователя

                        string subject = "Восстановление пароля на shoot-serv";

                        string url = $"http://shoot-serv-ru.1gb.ru/Account/RecoveryPassword?idUser={user.Id}&idRec={queryRecAdd.Data}";
                        string body = $"Вы выслали запрос на восстановление пароля на сервисе shoot-serv для вашего логина {login}. Ваш новый пароль {newPassword}. Для активации пароля перейдите по следующей ссылке {url}";

                        var sendResult = EmailSender.EmailHelper.SendMail(email, subject, body);
                        if (!sendResult)
                        {
                            /// TODO: Как - нибудь в лог запихнуть результат
                        }
                    }
                    else res = queryRecAdd.Result;
                }
                else
                {
                    res.IsOk = false;
                    res.ErrorMessage = "Не существует такого сочетания логина и электронной почты";
                }
            }
            else res.ErrorMessage = $"Не найден пользователь с логином {login}";

            return res;
        }

        // Метод активации неактивированного пароля
        public ResultInfo RecoveryPassword(int idUser, int idRec)
        {
            return _recoveryPasswordsLogic.RecoveryPassword(idUser, idRec);
        }
    }
}