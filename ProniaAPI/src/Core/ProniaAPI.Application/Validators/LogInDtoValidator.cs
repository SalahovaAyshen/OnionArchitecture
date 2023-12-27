using FluentValidation;
using ProniaAPI.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Validators
{
    public class LogInDtoValidator:AbstractValidator<LogInDto>
    {
        public const int MaxLength = 254;
        public const int MinLength = 4;
        public LogInDtoValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Username or Email can't be empty")
                .MinimumLength(MinLength).WithMessage("Username, Email or Password is incorrect")
                .MaximumLength(MaxLength).WithMessage("Username, Email or Password is incorrect");

        }
    }
}
