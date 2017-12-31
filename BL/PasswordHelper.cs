using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PasswordHelper
    {
        /// <summary>
        /// Получить рандомный пароль
        /// </summary>
        /// <returns></returns>
        public static string GetRandomPassword()
        {
            Random rnd = new Random();
            Char[] pwdChars = new Char[36] {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y' ,'z','0','1','2','3','4','5','6','7','8','9'};
            string res = String.Empty;
            for (int i = 0; i < 8; i++)
            res += pwdChars[rnd.Next(0, 35)];

            return res;
        }
    }
}
