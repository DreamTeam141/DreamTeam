using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Faculty.DAL.EF;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;

namespace Faculty.DAL.Repositories
{
    public class SubjectRepoitory : IRepository<Subject>
    {
        private ApplicationContext _database;

        public SubjectRepoitory(ApplicationContext database)
        {
            _database = database;
        }

        public IEnumerable<Subject> GetAll()
        {
            return _database.Subjects.ToList();
        }

        public Subject GetByTwoId(string userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByEntity(Subject item)
        {
            throw new NotImplementedException();
        }

        public Subject Get(int id)
        {
            return _database.Subjects.Find(id);
        }

        public Subject GetByStringId(string id)
        {
            return _database.Subjects.Find(Convert.ToInt32(id));
        }

        public IEnumerable<Subject> Find(Func<Subject, bool> predicate)
        {
            return _database.Subjects.Where(predicate).ToList();
        }

        public void Create(Subject item)
        {
            _database.Subjects.Add(item);
        }

        public void Update(Subject item)
        {
            _database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var subject = _database.Subjects.Find(id);
            if (subject != null)
            {
                subject.IsDeleted = true;
            }
        }
    }
}