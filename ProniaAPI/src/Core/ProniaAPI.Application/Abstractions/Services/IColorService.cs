using ProniaAPI.Application.DTOs.Categories;
using ProniaAPI.Application.DTOs.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take);
        Task Create(ColorCreateDto colorDto);
        Task Update(int id, string name);
    }
}
