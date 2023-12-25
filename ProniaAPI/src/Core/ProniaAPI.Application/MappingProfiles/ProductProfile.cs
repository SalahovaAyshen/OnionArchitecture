using AutoMapper;
using ProniaAPI.Application.DTOs.Categories;
using ProniaAPI.Application.DTOs.Products;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductItemDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
        }
    }
}
