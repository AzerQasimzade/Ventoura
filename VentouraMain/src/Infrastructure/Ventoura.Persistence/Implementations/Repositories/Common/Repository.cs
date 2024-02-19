using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ventoura.Application.Abstractions.Repositories;
using Ventoura.Domain.Entities;
using Ventoura.Persistence.DAL;

namespace Ventoura.Persistence.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
		public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool isTracking = false, params string[] includes)
		{
			IQueryable<T> query = _table;
			if (!isTracking) query = query.AsNoTracking();
			query = _addIncludes(query, includes);
			return await query.FirstOrDefaultAsync(predicate);
		}
		public IQueryable<T> GetAll(Expression<Func<T, 
            bool>>? expression = null, Expression<Func<T,
            object>>? orderExpression = null,
            bool isDescending = false,
            int skip = 0, int take = 0, bool isTracking = false,
            params string[] includes)
        {
            IQueryable<T> query = _table;
            if (expression != null) query = query.Where(expression);
            if (orderExpression != null)
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
            return isTracking ? query : query.AsNoTracking();
        }
        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false, int skip = 0, int take = 0, bool isTracking = false,params string[] includes)
        {
            IQueryable<T> query = _table;
            if (expression != null) query = query.Where(expression);
            if (orderExpression != null)
            {
                if (isDescending) query = query.OrderByDescending(orderExpression);
                else query = query.OrderBy(orderExpression);
            }
            if (skip != 0) query = query.Skip(skip);
            if (take != 0) query = query.Take(take);
            query = _addIncludes(query, includes);

            return isTracking ? query : query.AsNoTracking();
        }
        public async Task<T> GetByIdAsync(int id, bool isTracking = false,params string[] includes)
        {

            IQueryable<T> query = _table.Where(d => d.Id == id);
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>>? expression = null, bool isTracking = false,params string[] includes)
        {
            IQueryable<T> query = _table.Where(expression);
            if (!isTracking) query = query.AsNoTracking();
            query = _addIncludes(query, includes);
            return await query.FirstOrDefaultAsync();
        }
        public  async Task<List<Country>> GetAllCountriesAsync()
        {
            return await  _context.Countries.ToListAsync();
        }
        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Category.ToListAsync();
        }
        public async  Task<List<City>> GetAllCityAsync()
        {
            return await  _context.Cities.ToListAsync();
        }
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _table.Update(entity);
        }
        public void Delete(T entity)
        {
            _table.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
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

        public async Task<Tour> GettingThatObject(int id)
        {
            Tour tour=await _context.Tours.FirstOrDefaultAsync(c => c.Id == id);
            return tour;
        }

        public async Task AddWishlistItemAsync(WishlistItem item)
        {
            await _context.WishlistItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetProductCountAsync()
        {
            return await _context.Tours.CountAsync();
        }
    }
}
