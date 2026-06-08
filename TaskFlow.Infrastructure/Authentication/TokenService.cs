using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(User user)
        {
            var key = 
                new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            _configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new System.Security.Claims.Claim(ClaimTypes.Name, user.Name),
                new System.Security.Claims.Claim(ClaimTypes.Email, user.Email),
                new System.Security.Claims.Claim(ClaimTypes.Role, user.Role)
            };

            var token =
                new JwtSecurityToken(
                    issuer: 
                        _configuration["Jwt:Issuer"],
                    audience: 
                        _configuration["Jwt:Audience"],
                    claims: 
                        claims,
                    expires: 
                        DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: 
                        credentials
                    );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
