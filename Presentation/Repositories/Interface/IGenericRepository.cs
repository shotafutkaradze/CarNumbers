using Doamin.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task Update(T entity);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        Task<List<T>> GetAllPageing(
            /*Expression<Func<T, bool>> filter = null,*/
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            PageRequest pageRequest = null,
           params Expression<Func<T, object>>[] includes
          );
    }
}
