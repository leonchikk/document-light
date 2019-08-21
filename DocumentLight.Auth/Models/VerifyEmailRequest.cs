using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Auth.Models
{
    public class VerifyEmailRequest
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
    }
}
