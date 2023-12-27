using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Users;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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


        public async Task Login(LogInDto logInDto)
        {
            AppUser user = await _userManager.FindByNameAsync(logInDto.UserNameOrEmail);
            if(user is null)
            {
                user = await _userManager.FindByEmailAsync(logInDto.UserNameOrEmail);
                if (user is null) throw new Exception("Username, Email  or Password is incorrect");
            }
            if (!await _userManager.CheckPasswordAsync(user, logInDto.Password)) 
                throw new Exception("Username, Email  or Password is incorrect");
        }
    }
}
