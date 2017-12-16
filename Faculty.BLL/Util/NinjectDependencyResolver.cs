using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Mapper;
using Faculty.BLL.Services;
using Faculty.DAL;
using Faculty;
using Faculty.DAL.Interfaces;
using Faculty.Logger;
using Faculty.Logger.Interfaces;
using Ninject;

namespace Faculty.BLL.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISubjectService>().To<SubjectService>();
            kernel.Bind<ICourseService>().To<CourseService>();
            kernel.Bind<IStudentService>().To<StudentService>();
            kernel.Bind<ITeacherService>().To<TeacherService>();
            kernel.Bind<IJournalService>().To<JournalService>();

            kernel.Bind<IExceptionUnitOfWork>().To<ExceptionUnitOfWork>();
            kernel.Bind<IExceptionService>().To<ExceptionService>();
            kernel.Bind<IActionService>().To<ActionService>();
        }
    }
}