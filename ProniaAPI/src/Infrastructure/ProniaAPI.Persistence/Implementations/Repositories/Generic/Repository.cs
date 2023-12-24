using Microsoft.EntityFrameworkCore;
using ProniaAPI.Application.Abstractions.Repositories.Generic;
using ProniaAPI.Domain.Entities;
using ProniaAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null, bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
            bool ignoreQuery=false,
            params string[] includes)
        {
            IQueryable<T> query = _table;
            if (expression is not null) query = query.Where(expression);

            if (orderExpression is not null)
            {
                if (isDescending) query = query.OrderByDescending(orderExpression);

                else query = query.OrderBy(orderExpression);

            }
            if (skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            return isTracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T item = await _table.FirstOrDefaultAsync(i => i.Id == id);
            return item;
        }
        public async Task AddAsync(T item)
        {
            await _table.AddAsync(item);
        }
        public void SoftDelete(T item)
        {
            item.isDeleted= true;
            Update(item);
        }

        public void Update(T item)
        {
            _table.Update(item);
        }

        public void Delete(T item)
        {
            _table.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
