using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Notifications.Interfaces
{
    public interface IEmailService
    {
        void SendMail(MailAddress to, string body);
    }
}
