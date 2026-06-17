using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Domain.Entities;

namespace Application.Services
{
    public class AuthService(ICampaignRepository rep, IConfiguration conf) : IAuthService
    {
        public async Task<AuthResponse?> LoginAsync(LoginDto dto)
        {
            var cleanEmail = dto.Email.Trim();
            var cleanPassword = dto.Password.Trim();

            var agent = await rep.GetAgentByEmailAsync(cleanEmail);

            if (agent == null || !agent.PasswordHash.Equals(cleanPassword, StringComparison.Ordinal))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(conf["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, agent.Id.ToString()),
                new Claim(ClaimTypes.Name, agent.Name),
                new Claim(ClaimTypes.Email, agent.Email)
            }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = conf["Jwt:Issuer"],
                Audience = conf["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(tokenObj); 

            return new AuthResponse
            {
                Token = tokenString,
                Name = agent.Name,
                Email = agent.Email
            };
        }
    }
}

