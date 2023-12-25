﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Application.DTOs.Products;
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
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
           _repository = repository;
           _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAll(int page, int take)
        {
            return _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync());
        }
    }
}