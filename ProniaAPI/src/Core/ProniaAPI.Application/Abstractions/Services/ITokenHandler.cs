using ProniaAPI.Application.DTOs.Tokens;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateToken(AppUser user,IEnumerable<Claim> claims, int minutes);
    }
}
