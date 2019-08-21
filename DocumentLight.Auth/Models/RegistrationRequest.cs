using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DocumentLight.Auth.Models
{
    public class RegistrationRequest
    {
        [Required(ErrorMessage = "Password cannot be empty!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty!")]
        [RegularExpression(@"(?i:^\s*(?>(?:\w+\.?)*[a-z0-9]+)@[a-z]+\.[a-z]{2,}\s*$)", ErrorMessage = "Please, enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty!")]
        [MinLength(6, ErrorMessage = "Password should be minimum 6 characters long.")]
        [MaxLength(128, ErrorMessage = "Password cannot be more than 128 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "RedirectUrl cannot be empty!")]
        public string RedirectUrl { get; set; }
    }
}
