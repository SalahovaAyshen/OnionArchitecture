using ProniaAPI.Application.DTOs.Categories;
using ProniaAPI.Application.DTOs.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<TagItemDto>> GetAllAsync(int page, int take);
        Task Create(TagCreateDto tagDto);
        Task Update(int id, string name);
    }
}
