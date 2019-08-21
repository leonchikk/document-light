using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Auth.Models
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
        public string RedirectUrl { get; set; }
    }
}
