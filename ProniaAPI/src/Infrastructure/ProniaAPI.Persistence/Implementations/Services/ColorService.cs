using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Colors;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;

        public ColorService(IColorRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Color> colors = await _repository.GetAll(skip: (page - 1) * take, take: take).ToListAsync();

            ICollection<ColorItemDto> dtos = new List<ColorItemDto>();
            foreach (var color in colors)
            {
                dtos.Add(new ColorItemDto(color.Id, color.Name));
            }
            return dtos;
        }

        public async Task Create(ColorCreateDto colorDto)
        {
            await _repository.AddAsync(new Color
            {
                Name = colorDto.Name,
            });
            _repository.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            Color existed = await _repository.GetByIdAsync(id);
            if (existed == null) throw new Exception("Not Found Id");
            existed.Name = name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }

    }
}
