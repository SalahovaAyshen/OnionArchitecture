﻿using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.DTOs.Products
{
    public record ProductCreateDto(string Name, decimal Price, string SKU, string? Description,int CategoryId, ICollection<int>? ColorIds, ICollection<int>? TagIds);
    
}
