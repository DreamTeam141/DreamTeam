using System;
using System.Collections.Generic;

namespace Faculty.Logger.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        void Create(TEntity item);
    }
}