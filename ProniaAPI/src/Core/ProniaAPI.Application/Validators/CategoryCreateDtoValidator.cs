using FluentValidation;
using ProniaAPI.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Validators
{
    public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(100).WithMessage("Category name length can't be more than 100")
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("The category should contain only letters");
        }
    }
}
