using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentLight.Auth.Interfaces;
using DocumentLight.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            return Ok(_accountService.Login(request));
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationAsync(RegistrationRequest request)
        {
            return Ok(await _accountService.RegisterAsync(request));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            await _accountService.ForgotPasswordAsync(request);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _accountService.ResetPasswordAsync(request);
            return Ok();
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailRequest request)
        {
            await _accountService.VerifyEmailAsync(request);
            return Redirect(request.RedirectUrl);
        }
    }
}
