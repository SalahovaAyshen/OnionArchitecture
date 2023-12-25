using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions;
using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Colors;
using ProniaAPI.Application.DTOs.Tags;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Services
{
    public class TagService:ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;


        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            return _mapper.Map<ICollection<TagItemDto>>(tags);
        }

        public async Task Create(TagCreateDto tagDto)
        {
            await _repository.AddAsync(_mapper.Map<Tag>(tagDto));
            _repository.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            Tag existed = await _repository.GetByIdAsync(id, true);
            if (existed == null) throw new Exception("Not Found Id");
            existed.Name = name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }

    }
}
