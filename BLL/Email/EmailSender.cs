﻿using BLL.Validators;
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
            mail.From = new MailAddress("myemail");
            mail.To.Add(emailTemplate.To);
            
            if (emailTemplate.Attachment != null)
            {
                mail.Attachments.Add(emailTemplate.Attachment);
            }

            return mail;
        }

        public bool SendSmtpMail(EmailTemplate emailTemplate)
        {
            try
            {
                MailMessage message = GetMailMessage(emailTemplate);
                var credentials = new NetworkCredential("myemail", "mypassword");

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
                throw new MovieException("Failed to send an email");
            }
        }
    }
}
