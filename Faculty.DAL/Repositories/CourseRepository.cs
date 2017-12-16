using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Faculty.DAL.EF;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;

namespace Faculty.DAL.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private ApplicationContext _database;

        public CourseRepository(ApplicationContext database)
        {
            _database = database;
        }

        public Course GetByTwoId(string userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByEntity(Course item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetAll()
        {
            return _database.Courses.ToList();
        }

        public Course Get(int id)
        {
            return _database.Courses.Find(id);
        }

        public Course GetByStringId(string id)
        {
            return _database.Courses.Find(Convert.ToInt32(id));
        }

        public IEnumerable<Course> Find(Func<Course, Boolean> predicate)
        {
            return _database.Courses.Where(predicate).ToList();
        }

        public void Create(Course item)
        {
            _database.Courses.Add(item);
        }

        public void Update(Course item)
        {
            _database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var course = _database.Courses.Find(id);
            if (course != null)
            {
                course.IsDeleted = true;
            }
        }
    }
}