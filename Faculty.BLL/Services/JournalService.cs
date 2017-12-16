using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;

namespace Faculty.BLL.Services
{
    public class JournalService : IJournalService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;

        public JournalService(IUnitOfWork database, IMapper mapper, IStudentService studentService, ICourseService courseService, ITeacherService teacherService)
        {
            _database = database;
            _mapper = mapper;
            _studentService = studentService;
            _courseService = courseService;
            _teacherService = teacherService;
        }

        public void UpdateCourseStudent(CourseStudentDTO item)
        {
            var course = _database.CourseRepository.Get(item.CourseId);
            course.IsUpdated = true;
            _database.CourseRepository.Update(course);
            _database.Save();
            var courseStudent = _mapper.Map<CourseStudent>(item);
            _database.CourseStudentRepository.Update(courseStudent);
            _database.Save(); ;
        }

        public void Delete(CourseStudentDTO item)
        {
            var courseStudent = _mapper.Map<CourseStudent>(item);
            _database.CourseStudentRepository.DeleteByEntity(courseStudent);
            _database.Save();
        }

        public CourseStudentDTO GetCourseStudent(string userId, int courseId)
        {
            var courseStudent = _database.CourseStudentRepository.GetByTwoId(userId, courseId);
            var courseStudentDto = _mapper.Map<CourseStudentDTO>(courseStudent);
            return courseStudentDto;
        }
    }
}