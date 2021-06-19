using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GreenField.Common.Configuration;
using GreenField.DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GreenField.BLL.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtOptions> _options;

        public TokenService(IOptions<JwtOptions> options)
        {
            _options = options;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString("G")),
                new Claim("OrganisationId", user.OrganisationId.ToString())
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(_options.Value.TokenTimeLimitInMinutes)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.Secret)),
                    SecurityAlgorithms.HmacSha256));
            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}