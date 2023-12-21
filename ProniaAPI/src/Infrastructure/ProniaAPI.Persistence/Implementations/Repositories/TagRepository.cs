using ProniaAPI.Application.Abstractions;
using ProniaAPI.Domain.Entities;
using ProniaAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Repositories
{
    public class TagRepository:Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context):base(context) { }
       
    }
}
