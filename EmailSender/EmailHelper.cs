using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class EmailHelper
    {
        /// <summary>
        /// Отправить e-mail
        /// </summary>
        /// <param name="receiver">email получателя</param>
        /// <param name="subject">тема письма</param>
        /// <param name="body">тело письма</param>
        /// <returns></returns>
        public static bool SendMail(string receiver, string subject, string body)
        {
            bool res = false;

            string smtpHost = ConfigurationManager.AppSettings["smtpHost"];
            int smptPort = Convert.ToInt32( ConfigurationManager.AppSettings["smtpPort"] );
            string from = ConfigurationManager.AppSettings["mailFrom"];
            string password = ConfigurationManager.AppSettings["emailPassword"];
            string login = ConfigurationManager.AppSettings["emailLogin"];

            var client = new SmtpClient(smtpHost, smptPort);
            client.Credentials = new NetworkCredential(login, password);

            var mess = new MailMessage(from, receiver, subject, body);

            try
            {
                client.Send(mess);
                res = true;
            }
            catch (Exception ex)
            {
            }

            return res;
        }
    }
}
