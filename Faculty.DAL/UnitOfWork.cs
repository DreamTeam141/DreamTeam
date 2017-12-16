using System;
using Faculty.DAL.EF;
using Faculty.DAL.Entities;
using Faculty.DAL.Identity;
using Faculty.DAL.Interfaces;
using Faculty.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Faculty.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;
        private CourseRepository _courseRepository;
        private StudentRepository _studentRepository;
        private SubjectRepoitory _subjectRepoitory;
        private TeacherRepository _teacherRepository;
        private CourseStudentRepository _courseStudentRepository;

        public ApplicationUserManager UserManager
        {
            get { return _applicationUserManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _applicationRoleManager; }
        }

        public IRepository<Course> CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new CourseRepository(_context);
                }
                return _courseRepository;
            }
        }

        public IRepository<Student> StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_context);
                }
                return _studentRepository;
            }
        }

        public IRepository<Subject> SubjectRepository
        {
            get
            {
                if (_subjectRepoitory == null)
                {
                    _subjectRepoitory = new SubjectRepoitory(_context);
                }
                return _subjectRepoitory;
            }
        }

        public IRepository<Teacher> TeacherRepository
        {
            get
            {
                if (_teacherRepository == null)
                {
                    _teacherRepository = new TeacherRepository(_context);
                }
                return _teacherRepository;
            }
        }

        public IRepository<CourseStudent> CourseStudentRepository
        {
            get
            {
                if (_courseStudentRepository == null)
                {
                    _courseStudentRepository = new CourseStudentRepository(_context);
                }
                return _courseStudentRepository;
            }
        }

        public UnitOfWork()
        {
            _context = new ApplicationContext();
            _applicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            _applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationUserManager.Dispose();
                    _applicationRoleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}