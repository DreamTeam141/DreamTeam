using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Faculty.DAL.EF;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;

namespace Faculty.DAL.Repositories
{
    public class CourseStudentRepository : IRepository<CourseStudent>
    {
        private ApplicationContext _database;

        public CourseStudentRepository(ApplicationContext database)
        {
            _database = database;
        }

        public IEnumerable<CourseStudent> GetAll()
        {
            return _database.CourseStudents.ToList();
        }

        public CourseStudent Get(int id)
        {
            throw new NotImplementedException();
        }

        public CourseStudent GetByStringId(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CourseStudent> Find(Func<CourseStudent, bool> predicate)
        {
            return _database.CourseStudents.Where(predicate).ToList();
        }

        public void Create(CourseStudent item)
        {
            _database.CourseStudents.Add(item);
        }

        public CourseStudent GetByTwoId(string userId, int courseId)
        {
            return _database.CourseStudents.First(x => x.CourseId == courseId && x.StudentId == userId);
        }

        public void DeleteByEntity(CourseStudent item)
        {
            //var cours
            var courseStudent = _database.CourseStudents
                .Where(x => x.CourseId == item.CourseId && x.StudentId == item.StudentId).FirstOrDefault();
            _database.Entry(courseStudent).State = EntityState.Deleted;
        }

        public void Update(CourseStudent item)
        {
            _database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}