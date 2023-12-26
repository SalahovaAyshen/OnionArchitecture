using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions;
using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Products;
using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Services
{
    internal class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository,IColorRepository colorRepository,ITagRepository tagRepository,IMapper mapper)
        {
           _repository = repository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAll(int page, int take)
        {
            return _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync());
        }
        public async Task<ProductGetDto> GetByIdAsync(int id)
        {
            Product product = await _repository.GetByIdAsync(id,includes:nameof(Product.Category));
            if (product is null) throw new Exception("Not Found");
            return _mapper.Map<ProductGetDto>(product);
        }
        public async Task CreateAsync(ProductCreateDto createDto)
        {
            if (await _repository.IsExisted(x => x.Name == createDto.Name)) throw new Exception("Such a product name already exists");
            if (!await _categoryRepository.IsExisted(x => x.Id == createDto.CategoryId)) throw new Exception("Not Found Category Id");
            Product product = _mapper.Map<Product>(createDto);
            product.ProductColors = new List<ProductColor>();
            if(createDto.ColorIds is not null)
            {
                foreach (var colId in createDto.ColorIds)
                {
                    if (!await _colorRepository.IsExisted(x => x.Id == colId)) throw new Exception("Not Found color Id");
                    product.ProductColors.Add(new ProductColor { ColorId = colId });
                }
            }
            product.ProductTags = new List<ProductTag>();
            if (createDto.TagIds is not null)
            {
                foreach (var tagId in createDto.TagIds)
                {
                    if (!await _tagRepository.IsExisted(x => x.Id == tagId)) throw new Exception("Not Found Tag id");
                    product.ProductTags.Add(new ProductTag { TagId = tagId });
                }
            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, ProductUpdateDto dto)
        {
            Product existed = await _repository.GetByIdAsync(id, includes:nameof(Product.ProductColors));
            if (existed is null) throw new Exception("Not found product id");
            if(dto.Name!=existed.Name)
                if (await _repository.IsExisted(x => x.Name == dto.Name)) throw new Exception("Such a product name already exists");
            if (dto.CategoryId != existed.CategoryId)
                if (!await _categoryRepository.IsExisted(x => x.Id == dto.CategoryId)) throw new Exception("Not Found Category Id");
            existed = _mapper.Map(dto,existed);

            
            if(dto.ColorIds is not null)
            {
                foreach (var colorId in dto.ColorIds)
                {
                    if (!existed.ProductColors.Any(pc => pc.ColorId == colorId))
                    {
                        if (!await _colorRepository.IsExisted(x => x.Id == colorId)) throw new Exception("Not Found color Id");
                        existed.ProductColors.Add(new ProductColor { ColorId = colorId });
                    }
                }
                existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colId => pc.ColorId == colId)).ToList();

            }
            else
                existed.ProductColors = new List<ProductColor>();
            if (dto.TagIds is not null)
            {
                foreach (var tagId in dto.TagIds)
                {
                    if (!existed.ProductTags.Any(pc => pc.TagId == tagId))
                    {
                        if (!await _tagRepository.IsExisted(x => x.Id == tagId)) throw new Exception("Not Found color Id");
                        existed.ProductTags.Add(new ProductTag { TagId = tagId });
                    }
                }
                existed.ProductTags = existed.ProductTags.Where(pc => dto.TagIds.Any(tId => pc.TagId == tId)).ToList();

            }
            else
                existed.ProductTags = new List<ProductTag>();

            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDelete(int id)
        {
            Product existed = await _repository.GetByIdAsync(id);
            if (existed is null) throw new Exception("Not found product id");
            _repository.SoftDelete(existed);
           await _repository.SaveChangesAsync();

        }
        public async Task ReverseDelete(int id)
        {
            Product existed = await _repository.GetByIdAsync(id);
            if (existed is null) throw new Exception("Not found product id");
            _repository.ReverseDelete(existed);
            await _repository.SaveChangesAsync();

        }
        public async Task Delete(int id)
        {
            Product existed = await _repository.GetByIdAsync(id,ignoreQuery:true);
            if (existed is null) throw new Exception("Not found product id");
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }
    }
}
