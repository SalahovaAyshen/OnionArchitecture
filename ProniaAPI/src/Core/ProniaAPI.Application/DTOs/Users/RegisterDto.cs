using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.DTOs.Users
{
    public record RegisterDto(string UserName, string Email, string Password, string ConfirmPassword, string Name, string Surname);

}
