using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Domain.Entities;

namespace Ventoura.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
            Expression<Func<T,
            bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = false,
            params string[] includes);
        IQueryable<T> GetAllWhere(
           Expression<Func<T, bool>>? expression = null,
           Expression<Func<T, object>>? orderExpression = null,
           bool isDescending = false,
           int skip = 0,
           int take = 0,
           bool isTracking = false,
           params string[] includes);
        Task<T> GetByIdAsync(
           int id,
           bool isTracking = false,
           params string[] includes);
        Task<Tour> GettingThatObject(int id);
        Task<T> GetByExpressionAsync(
           Expression<Func<T, bool>>? expression = null,
           bool isTracking = false,
           params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = false, params string[] includes);
		


	}
}
