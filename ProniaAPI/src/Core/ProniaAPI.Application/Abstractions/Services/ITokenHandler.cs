using ProniaAPI.Application.DTOs.Tokens;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateToken(AppUser user, int minutes);
    }
}
