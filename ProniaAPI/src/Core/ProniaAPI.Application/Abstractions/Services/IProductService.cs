using ProniaAPI.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>> GetAll(int page, int take);
        Task<ProductGetDto> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateDto createDto);
        Task UpdateAsync(int id, ProductUpdateDto dto);
        Task SoftDelete(int id);
        Task ReverseDelete(int id);
        Task Delete(int id);

    }
}
