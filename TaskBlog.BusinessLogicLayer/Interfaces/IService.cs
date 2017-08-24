using System.Collections.Generic;

namespace TaskBlog.BusinessLogicLayer.Interfaces
{
    public interface IService<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        void Create(TEntity model);
        void Update(TEntity model);
        void Delete(object id);
        void Save();
    }
}
