using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocumentLight.Auth.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email cannot be empty!")]
        [RegularExpression(@"(?i:^\s*(?>(?:\w+\.?)*[a-z0-9]+)@[a-z]+\.[a-z]{2,}\s*$)", ErrorMessage = "Please, enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty!")]
        public string Password { get; set; }
    }
}
