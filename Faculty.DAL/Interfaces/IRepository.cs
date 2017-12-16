using System;
using System.Collections.Generic;

namespace Faculty.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity GetByTwoId(string userId, int courseId);
        TEntity GetByStringId(string id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        void DeleteByEntity(TEntity item);
    }
}