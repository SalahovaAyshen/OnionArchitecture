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
            bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes);
        IQueryable<T> GetAllWhere(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null, bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
            bool ignoreQuery=false,
            params string[] includes);
        Task<bool> IsExisted(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(
            int id, 
            bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes);
        Task<T> GetByExpression(
             Expression<Func<T, bool>>? expression = null,
             bool isTracking = true,
            bool ignoreQuery = false,
            params string[] includes);
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        void SoftDelete(T item);
        void ReverseDelete(T item);
        Task SaveChangesAsync();
    }
}
