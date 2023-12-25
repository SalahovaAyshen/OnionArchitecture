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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public IQueryable<T> GetAll(bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table;
            query = _ignoreQuery(query, ignoreQuery); 
            query = _isTracking(query, isTracking);
            query = _addIncludes(query, includes);
            return query;
        }
        public IQueryable<T> GetAllWhere(
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
            query = _addIncludes(query, includes);
            query = _isTracking(query, isTracking);
            query = _ignoreQuery(query, ignoreQuery); ;
            return query;
        }

        public async Task<T> GetByIdAsync(int id, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(x=>x.Id==id);
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            query = _isTracking(query, isTracking);
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();

        }
        public async Task<T> GetByExpression(Expression<Func<T, bool>>? expression = null, bool isTracking = true, bool ignoreQuery = false, params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);
            query=_ignoreQuery(query,ignoreQuery);
            query = _isTracking(query, isTracking);
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
        public async Task AddAsync(T item)
        {
            await _table.AddAsync(item);
        }
        public void Update(T item)
        {
            _table.Update(item);
        }
        public void SoftDelete(T item)
        {
            item.isDeleted= true;
        }
        public void ReverseDelete(T item)
        {
            item.isDeleted = false;
        }
        public void Delete(T item)
        {
            _table.Remove(item);
        }
        public async Task SaveChangesAsync()
        {
            _context.SaveChanges();
        }

        private IQueryable<T> _addIncludes(IQueryable<T> query, params string[] includes)
        {
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;

        }
        private IQueryable<T> _isTracking(IQueryable<T> query,bool isTracking)
        {
            if (isTracking) query = query.AsTracking();
            return query;
        }
        private IQueryable<T> _ignoreQuery(IQueryable<T> query, bool ignoreQuery)
        {
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            return query;
        }
     

      

       

      
    }
}
