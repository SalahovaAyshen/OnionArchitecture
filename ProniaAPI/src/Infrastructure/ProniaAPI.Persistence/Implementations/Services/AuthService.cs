using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Users;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }

      

        public async Task Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName || u.Email == registerDto.Email)) throw new Exception("Username or email must be unique");

            AppUser user = _mapper.Map<AppUser>(registerDto);

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (IdentityError error in result.Errors)
                {
                    stringBuilder.AppendLine(error.Description);
                }
                throw new Exception(stringBuilder.ToString());
            }
        }


        public async Task<string> Login(LogInDto logInDto)
        {
            AppUser user = await _userManager.FindByNameAsync(logInDto.UserNameOrEmail);
            if(user is null)
            {
                user = await _userManager.FindByEmailAsync(logInDto.UserNameOrEmail);
                if (user is null) throw new Exception("Username, Email  or Password is incorrect");
            }
            if (!await _userManager.CheckPasswordAsync(user, logInDto.Password)) 
                throw new Exception("Username, Email  or Password is incorrect");

            ICollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname)
             };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddMinutes(60),
                claims:claims,
                signingCredentials:credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
            
        }
    }
}
