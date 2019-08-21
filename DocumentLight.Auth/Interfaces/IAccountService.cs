using DocumentLight.Auth.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.Auth.Interfaces
{
    public interface IAccountService
    {
        AuthorizationToken Login(LoginRequest request);
        Task<AuthorizationToken> RegisterAsync(RegistrationRequest request);
        Task ForgotPasswordAsync(ForgotPasswordRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
        Task VerifyEmailAsync(VerifyEmailRequest reruqest);
    }
}
