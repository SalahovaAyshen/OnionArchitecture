using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
    }
}
