using Axeso_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Axeso_DA
{
    public class MailHelper
    {
        public static async Task SendMail(MailModel mailmodel)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(mailmodel.to));
            message.From = new MailAddress(mailmodel.AdminUser);
            message.Subject = mailmodel.subject;
            message.Body = mailmodel.body;
            message.IsBodyHtml = true;


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = mailmodel.AdminUser,
                    Password = mailmodel.AdminPassWord
                };

                try
                {
                    smtp.Credentials = credential;
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = mailmodel.SMTPName;
                    smtp.Port = int.Parse(mailmodel.SMTPPort);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    message.Dispose();
                    smtp.Dispose();
                }
                catch (Exception ex)
                {
                    var x = 1;
                }
            }
        }
    }
}
