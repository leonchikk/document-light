using Common.Core.Messages;
using DocumentLight.Auth.Helpers;
using DocumentLight.Auth.Interfaces;
using DocumentLight.Auth.Models;
using DocumentLight.Core.Entities;
using DocumentLight.Core.Interfaces;
using EasyNetQ;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentLight.Auth.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IBus _serviceBus;
        private readonly IHttpContextAccessor _accessor;

        public AccountService(IUnitOfWork unitOfWork, ITokenService tokenService, IBus serviceBus,
                              IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _serviceBus = serviceBus;
            _accessor = accessor;
        }

        public AuthorizationToken Login(LoginRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(x => x.Email == request.Email && x.Password == CryptographyHelper.EncryptPswd(request.Password)).FirstOrDefault();

            if (user == null)
                throw new Exception("Incorrect email or password!");

            if (!user.IsEmailVerified)
                throw new Exception("Email is not verified for this user!");

            return _tokenService.CreateToken(user);
        }

        public async Task<AuthorizationToken> RegisterAsync(RegistrationRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(x => x.Email == request.Email).FirstOrDefault();

            if (user != null)
                throw new Exception("User with that email already exist!");

            user = new User(request.Email, request.FullName, CryptographyHelper.EncryptPswd(request.Password));
            await _unitOfWork.UsersRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();
            
            var callbackUrl = UrlHelper.AddUrlParameters(
                url: $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/accounts/verify-email", 
                parameters: new Dictionary<string, string>
                {
                    { "token", user.VerifyEmailToken },
                    { "redirectUrl", request.RedirectUrl }
                });

            await _serviceBus.PublishAsync(new SendMail() { To = user.Email, Text = callbackUrl });
            return _tokenService.CreateToken(user);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(x => x.Email == request.Email).FirstOrDefault();

            if (user == null)
                throw new Exception("User with that email does not exist!");

            var forgotPasswordToken = Guid.NewGuid().ToString();
            user.ForgotPasswordToken = forgotPasswordToken;
            await _unitOfWork.SaveAsync();

            var callbackUrl = UrlHelper.AddUrlParameters(
                url: request.RedirectUrl, 
                parameters: new Dictionary<string, string>
                {
                    { "token", forgotPasswordToken }
                });

            await _serviceBus.PublishAsync(new SendMail() { To = request.Email, Text = callbackUrl });
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(x => x.ForgotPasswordToken == request.ForgotPasswordToken).FirstOrDefault();

            if (user == null)
                throw new Exception("Forgot password token is not valid!");

            user.Password = CryptographyHelper.EncryptPswd(request.Password);
            user.ForgotPasswordToken = null;

            await _unitOfWork.SaveAsync();
        }

        public async Task VerifyEmailAsync(VerifyEmailRequest request)
        {
            var user = _unitOfWork.UsersRepository.FindBy(x => x.VerifyEmailToken == request.Token).FirstOrDefault();

            if (user == null)
                throw new Exception("User does not exist!");

            user.IsEmailVerified = true;
            await _unitOfWork.SaveAsync();
        }
    }
}
