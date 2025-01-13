using ECommerce.Identity.Application.Contracts;
using ECommerce.Identity.Domain.Aggregates;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Infrastructure.Services
{
    public class AccountService(JwtSettings jwtSettings) : IAccountService
    {
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString())
           };

            //1. buradaki gizli anahtar:
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,//2. Issuer bilgisi
                audience: jwtSettings.Audience, // 3. Audience bilgisi
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(jwtSettings.ExpirationInHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
