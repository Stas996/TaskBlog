using System;
using TaskBlog.DataLayer;

namespace TaskBlog.DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Save();
    }
}
