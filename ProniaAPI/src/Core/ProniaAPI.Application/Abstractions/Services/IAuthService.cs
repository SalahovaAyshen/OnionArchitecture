using ProniaAPI.Application.DTOs.Tokens;
using ProniaAPI.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto registerDto);
        Task<TokenResponseDto> Login(LogInDto logInDto);
    }
}
