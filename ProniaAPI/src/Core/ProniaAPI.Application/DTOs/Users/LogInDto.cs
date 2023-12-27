using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.DTOs.Users
{
    public record LogInDto(string UserNameOrEmail, string Password);

}
