using Microsoft.Extensions.Configuration;
using Notifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Notifications.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_configuration.GetSection("SmtpClientSettings:UserName").Value, _configuration.GetSection("SmtpClientSettings:Password").Value),
                EnableSsl = true
            };
        }

        public void SendMail(MailAddress to, string body)
        {
            var msg = new MailMessage
            {
                From = new MailAddress(_configuration.GetSection("SmtpClientSettings:UserName").Value, "DocumentCRM"),
                Subject = "Email verification",
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            };
            msg.To.Add(to);

            _smtpClient.Send(msg);
        }
    }
}
