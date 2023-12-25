using ProniaAPI.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.DTOs.Products
{
    public record ProductGetDto(int Id, string Name, decimal Price, string? Description, string Sku, int CategoryId, IncludeCategoryDto Category);
   
}
