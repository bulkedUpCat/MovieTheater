using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Email
{
    public class EmailSender
    {
        private static MailMessage GetMailMessage(EmailTemplate emailTemplate)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.Body = emailTemplate.Body;
            mail.Subject = emailTemplate.Subject;
            mail.From = new MailAddress("youremail@gmail.com");
            mail.To.Add(emailTemplate.To);
            return mail;
        }

        public bool SendSmtpMail(EmailTemplate emailTemplate)
        {
            try
            {
                MailMessage message = GetMailMessage(emailTemplate);
                var credentials = new NetworkCredential("youremail@gmail.com", "yourpassword");

                var client = new SmtpClient()
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = credentials,
                    Host = "smtp.office365.com",
                    EnableSsl = true
                };

                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
