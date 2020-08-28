using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FC.Provider.Providers
{
    public interface IGenericRepositories<T> where T: class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> FindList(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> predicate);

        T FindById(Expression<Func<T, bool>> predicate);
        Task<T> FindByIdAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
