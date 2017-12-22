using System;
using System.Collections.Generic;
using System.Linq;
using Faculty.Logger.EF;
using Faculty.Logger.Entities;
using Faculty.Logger.Interfaces;

namespace Faculty.Logger.Repositories
{
    public class ExceptionDetailRepository : IRepository<ExceptionDetail>
    {
        private ExceptionDetailContext _database;

        public ExceptionDetailRepository(ExceptionDetailContext context)
        {
            _database = context;
        }

        public IEnumerable<ExceptionDetail> GetAll()
        {
            return _database.ExceptionDetails.ToList();
        }

        public ExceptionDetail Get(int id)
        {
            return _database.ExceptionDetails.First(x => x.Id == id);
        }

        public IEnumerable<ExceptionDetail> Find(Func<ExceptionDetail, bool> predicate)
        {
            return _database.ExceptionDetails.Where(predicate).ToList();
        }

        public void Create(ExceptionDetail item)
        {
            _database.ExceptionDetails.Add(item);
        }
    }
}