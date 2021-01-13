using BookStore.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class EmailService
    {
        private readonly string templatePath = @"EmailTemplates/{0}.html";
        private readonly SmtpConfiguration _smtpOptions;

        public EmailService(IOptions<SmtpConfiguration> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendTestEmail(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = "Test email subject";
            emailOptions.Body = GetEmailBody("TestEmail");

            await SendEmail(emailOptions);
        }

        private async Task SendEmail(UserEmailOptions emailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = emailOptions.Subject,
                Body = emailOptions.Body,
                From = new MailAddress(_smtpOptions.SenderAddress, _smtpOptions.SenderDisplayName),
                IsBodyHtml = _smtpOptions.IsBodyHTML,
                BodyEncoding = Encoding.Default
            };

            foreach (var email in emailOptions.ToEmails)
            {
                mail.To.Add(email);
            }

            var credentials = new NetworkCredential(_smtpOptions.UserName, _smtpOptions.Password);

            var client = new SmtpClient()
            {
                Host = _smtpOptions.Host,
                Port = _smtpOptions.Port,
                EnableSsl = _smtpOptions.EnableSSL,
                UseDefaultCredentials = _smtpOptions.UseDefaultCredentials,
                Credentials = credentials               
            };

            await client.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            
            return body;
        }

    }
}
