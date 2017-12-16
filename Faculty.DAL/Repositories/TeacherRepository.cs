using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Faculty.DAL.EF;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;

namespace Faculty.DAL.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private ApplicationContext _database;

        public TeacherRepository(ApplicationContext database)
        {
            _database = database;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _database.Teachers.ToList();
        }

        public Teacher Get(int id)
        {
            throw new NotImplementedException();
        }

        public Teacher GetByTwoId(string userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByEntity(Teacher item)
        {
            throw new NotImplementedException();
        }

        public Teacher GetByStringId(string id)
        {
            return _database.Teachers.First(x => x.Id == id);
        }

        public IEnumerable<Teacher> Find(Func<Teacher, bool> predicate)
        {
            return _database.Teachers.Where(predicate).ToList();
        }

        public void Create(Teacher item)
        {
            _database.Entry(item).State = EntityState.Added;
        }

        public void Update(Teacher item)
        {
            _database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}