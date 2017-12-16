using System;
using System.Collections.Generic;
using System.Linq;
using Faculty.Logger.EF;
using Faculty.Logger.Entities;
using Faculty.Logger.Interfaces;

namespace Faculty.Logger.Repositories
{
    public class ActionDetailRepository : IRepository<ActionDetail>
    {
        private ExceptionDetailContext _database;

        public ActionDetailRepository(ExceptionDetailContext database)
        {
            _database = database;
        }

        public IEnumerable<ActionDetail> GetAll()
        {
            return _database.ActionDetails.ToList();
        }

        public ActionDetail Get(int id)
        {
            return _database.ActionDetails.Find(id);
        }

        public IEnumerable<ActionDetail> Find(Func<ActionDetail, bool> predicate)
        {
            return _database.ActionDetails.Where(predicate).ToList();
        }

        public void Create(ActionDetail item)
        {
            _database.ActionDetails.Add(item);
        }
    }
}