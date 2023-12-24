using ProniaAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null, bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
            bool ignoreQuery=false,
            params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        void SoftDelete(T item);

        Task SaveChangesAsync();
    }
}
