using Common.Core.Messages;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Notifications.Interfaces;
using Notifications.Services;
using System;
using System.IO;
using System.Net.Mail;

namespace Notifications
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            IEmailService emailService = new EmailService(configuration);
            var messageBus = RabbitHutch.CreateBus($"host={configuration.GetSection("RabbitMqHost").Value}");

            messageBus.Subscribe<SendMail>(Guid.NewGuid().ToString(), msg => emailService.SendMail(new MailAddress(msg.To), msg.Text));
            
        }
    }
}
