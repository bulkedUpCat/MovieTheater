using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstractions.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<T> GetByIdAsync(object id);

        Task<T> InsertAsync(T entity);

        Task DeleteByIdAsync(int id);
        void Update(T entityToUpdate);
    }
}
