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
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
