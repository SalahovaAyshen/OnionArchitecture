using FluentValidation;
using FluentValidation.AspNetCore;
using ProniaAPI.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        private const int MaxEmailOrUserNameLength = 254;
        private const int MinPasswordLength = 8;
        private const int MinUserNameLength = 4;
        private const int MinNameOrSurnameLength = 3;
        private const int MaxNameLength = 25;
        private const int MaxSurnameLength = 30;

        public RegisterDtoValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email length can't be empty")
                .Matches(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])*$")
                .MaximumLength(MaxEmailOrUserNameLength).WithMessage("Email length can't be more than 254");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .Must(CheckPassword).WithMessage("Password must contain min 'A-Z', min 'a-z', min '0-9'")
                .MinimumLength(MinPasswordLength);
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Username can't be empty")
                .MinimumLength(MinUserNameLength).WithMessage("Username length can't be less than 4 characters")
                .MaximumLength(MaxEmailOrUserNameLength).WithMessage("Username length can't be more than 254 characters");
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .Matches(@"^[a-zA-z\s]*$").WithMessage("Name must contain just letters")
                .MinimumLength(MinNameOrSurnameLength).WithMessage("Name length can't be less than 3 letters")
                .MaximumLength(MaxNameLength).WithMessage("Name length can't be more than 25 letters");
            RuleFor(r => r.Surname)
               .NotEmpty().WithMessage("Surname can't be empty")
               .Matches(@"^[a-zA-z\s]*$").WithMessage("Surname must contain just letters")
               .MinimumLength(MinNameOrSurnameLength).WithMessage("Surname length can't be less than 3 letters")
               .MaximumLength(MaxSurnameLength).WithMessage("Surname length can't be more than 30 letters");
            RuleFor(r => r).Must(r => r.ConfirmPassword == r.Password);

        }
        public static bool CheckPassword(string password)
        {
            int upper = 0;
            int lower = 0;
            int digit = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsUpper(password[i]))
                {
                    upper++;
                }
                else if (Char.IsLower(password[i]))
                {
                    lower++;
                }
                else if (Char.IsDigit(password[i]))
                {
                    digit++;
                }
            }
            if(upper>0 && lower>0 && digit > 0)
            {
                return true;
            }
            return false;
        }
    }
}
