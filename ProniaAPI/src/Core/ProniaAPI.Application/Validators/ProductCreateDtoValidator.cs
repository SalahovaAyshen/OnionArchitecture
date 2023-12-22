using FluentValidation;
using ProniaAPI.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Validators
{
    public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
    {
        private const int Maxlength = 100;
        private const int Minlength = 2;
        private const int Sku = 6;
        public ProductCreateDtoValidator()
        { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can't be empty")
                .MaximumLength(Maxlength).WithMessage("The product name length can't be more than 100")
                .MinimumLength(Minlength).WithMessage("the product name length can't be less than 2");
            RuleFor(x => x.SKU).NotEmpty().WithMessage("The product sku can't be empty")
                .Must(s => s.Length == Sku).WithMessage("The product sku must contain only 6 characters");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price can't be empty").Must(CheckPrice);
        }
        public bool CheckPrice(decimal price)
        {
            if(price>=10 && price < 999999.99m)
            {
                return true;
            }
            return false;
        }
    }
}
