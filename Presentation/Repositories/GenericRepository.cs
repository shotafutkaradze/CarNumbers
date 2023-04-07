using Doamin.Entities;
using Doamin.Helper;
using Doamin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CarBDContext _context;
        public GenericRepository(CarBDContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllPageing(/*Expression<Func<T, bool>> filter = null,*/ Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, PageRequest pageRequest = null, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));

            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            //if (filter != null)
            //{
            //    query = query.AsExpandable().Where(filter);
            //}
            if (pageRequest != null)
            {
                query = query.Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize).Take(pageRequest.PageSize);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task Update(T entity)
        {
             _context.Set<T>().Update(entity);
        }
       
    }
}
