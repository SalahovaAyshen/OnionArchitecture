using AutoMapper;
using ProniaAPI.Application.DTOs.Categories;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.MappingProfiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryItemDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, IncludeCategoryDto>().ReverseMap();
        }
    }
}
