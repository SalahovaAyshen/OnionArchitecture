using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Tokens;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Infrastructure.Implementations.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _config;

        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }
        public TokenResponseDto CreateToken(AppUser user, IEnumerable<Claim> claims,int minutes)
        {
           
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(minutes),
                claims: claims,
                signingCredentials: credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(token),user.UserName,token.ValidTo);
        }
        public string CreateRefreshToken()
        {
            //byte[] bytes = new byte[32];
            //var random = RandomNumberGenerator.Create();
            //random.GetBytes(bytes);
            //return Convert.ToBase64String(bytes);
            return Guid.NewGuid().ToString();
        }
    }
}
