using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Categories;
using ProniaAPI.Domain.Entities;

namespace ProniaAPI.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<CategoryItemDto>>(categories); 
        }

        public async Task Create(CategoryCreateDto categoryDto)
        {
            await _repository.AddAsync(_mapper.Map<Category>(categoryDto));
             _repository.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("Not Found Id");
            existed.Name = name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id, true);
            if (category == null) throw new Exception("Not Found");
            _repository.SoftDelete(category);
            await _repository.SaveChangesAsync();  
        }
    }
}
