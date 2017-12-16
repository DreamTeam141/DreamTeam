using System;
using Faculty.DAL.Entities;
using Faculty.DAL.Identity;

namespace Faculty.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationRoleManager RoleManager { get; }
        ApplicationUserManager UserManager { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<Student> StudentRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        IRepository<Teacher> TeacherRepository { get; }
        IRepository<CourseStudent> CourseStudentRepository { get; }
        void Save();
    }
}