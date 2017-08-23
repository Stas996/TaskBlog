using System.Collections.Generic;

namespace TaskBlog.BusinessLogicLayer.Interfaces
{
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Create(TEntity model);
        void Update(TEntity model);
        void Delete(int id);
        void Save();
    }
}
