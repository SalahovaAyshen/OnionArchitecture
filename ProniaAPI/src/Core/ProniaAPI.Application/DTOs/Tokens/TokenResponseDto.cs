using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.DTOs.Tokens
{
    public record TokenResponseDto(string Token, string UserName, DateTime ExpiredAt);
}
