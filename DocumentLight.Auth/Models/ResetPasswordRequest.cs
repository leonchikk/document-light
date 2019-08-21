using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocumentLight.Auth.Models
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string ForgotPasswordToken { get; set; }
    }
}
