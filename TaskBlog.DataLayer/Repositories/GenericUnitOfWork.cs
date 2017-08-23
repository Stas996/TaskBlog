using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskBlog.DataLayer
{
    public class GenericUnitOfWork : IUnitOfWork
    {
        protected DbContext _dbContext;
        protected Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public GenericUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }
            IRepository<TEntity> repo = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repo);
            return repo;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GenericUnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
