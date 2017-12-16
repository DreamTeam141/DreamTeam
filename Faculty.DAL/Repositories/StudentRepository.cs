using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Faculty.DAL.EF;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;

namespace Faculty.DAL.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        private ApplicationContext _database;

        public StudentRepository(ApplicationContext database)
        {
            _database = database;
        }

        public IEnumerable<Student> GetAll()
        {
            return _database.Students.ToList();
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetByTwoId(string userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByEntity(Student item)
        {
            throw new NotImplementedException();
        }

        public void DeleteByTwoId(string userId, int coutseId)
        {
            throw new NotImplementedException();
        }

        public Student GetByStringId(string id)
        {
            return _database.Students.First(x => x.Id == id);
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return _database.Students.Where(predicate).ToList();
        }

        public void Create(Student item)
        {
            _database.Entry(item).State = EntityState.Added;
        }

        public void Update(Student item)
        {
            _database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}