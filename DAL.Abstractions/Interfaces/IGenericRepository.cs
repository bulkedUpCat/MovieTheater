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
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetByID(object id);

        void Insert(T entity);

        void Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        IEnumerable<T> Where(Expression<Func<T, bool>> expression);
        bool Any(Expression<Func<T, bool>> expression = null);
        T FirstOrDefault(Expression<Func<T, bool>> expression);
    }
}
