using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Domain.Entities;
using ProniaAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Repositories
{
    public class ProductRepository:Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context) { }
    }
}
