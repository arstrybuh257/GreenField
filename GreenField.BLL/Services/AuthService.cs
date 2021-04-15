using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GreenField.BLL.Services
{
    public class AuthService : IAuthService
    {
        public string CreateToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(30)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MySecretSecretGreenField")),
                    SecurityAlgorithms.HmacSha256));
            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}