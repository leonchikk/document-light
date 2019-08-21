using DocumentLight.Auth.Interfaces;
using DocumentLight.Auth.Models;
using DocumentLight.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocumentLight.Auth.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthorizationToken CreateToken(User user)
        {
            var identity = GetIdentity(user);

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("Authentication:Issuer").Value,
                    audience: _configuration.GetSection("Authentication:Audience").Value,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(_configuration.GetSection("Authentication:LifeTime").Value))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:Key").Value)),
                                                                                                                            SecurityAlgorithms.HmacSha256));

            var encodedJwt = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthorizationToken
            {
                Token = encodedJwt
            };
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                   new Claim("Email", user.Email)
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
