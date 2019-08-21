using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Core.Entities
{
    public class User: BaseEntity
    {
        // For EF
        private User() { }
        public User(string email, string fullname, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            FullName = fullname;
            Password = password;
            VerifyEmailToken = Guid.NewGuid().ToString();
            IsEmailVerified = false;
        }

        public string ForgotPasswordToken { get; set; }
        public string VerifyEmailToken { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
