using System;
using System.Collections.Generic;

namespace TaskBlog.DataLayer
{
    public interface IRepository<T>// : IDisposable
        where T : class
    {
        void Create(T item);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Func<T, bool> predicate);
        void Delete(int id);
        void Delete(T item);
        void Update(T item);
        void Save();
    }
}
