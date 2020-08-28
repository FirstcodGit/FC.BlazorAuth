using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FC.Provider.Providers
{
    public class GenericRepositories<T> : IDisposable, IGenericRepositories<T> where T : class
    {
        protected readonly DbContext _context;

        protected readonly DbSet<T> _entity;

        public GenericRepositories(DbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.ToList();
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns>await ToListAsync()</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public IEnumerable<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).ToList();
        }

        /// <summary>
        /// Async ToListAsync(value?)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>await ToListAsync()</returns>
        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.Where(predicate).ToListAsync();
        }

        public T FindById(Expression<Func<T, bool>> predicate)
        {
            return _entity.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Single Result
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>await SingleOrDefaultAsync()</returns>
        public async Task<T> FindByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _entity.Add(entity);
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }

        public void Update(T entity)
        {
            _entity.Update(entity);
        }

        #region IDisposable Support
        
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
