using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Faculty.BLL.DTO;
using Faculty.BLL.Interfaces;
using Faculty.BLL.Util;
using Faculty.DAL.Entities;
using Faculty.DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace Faculty.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        public void Create(CourseDTO courseDto)
        {
            var subject = _database.SubjectRepository.Get(courseDto.Subject.Id);
            var teacher = _database.TeacherRepository.GetByStringId(courseDto.Teacher.Id);
            var course = _mapper.Map<Course>(courseDto);
            course.Subject = subject;
            course.Teacher = teacher;
            _database.CourseRepository.Create(course);
            _database.Save();
        }

        public IEnumerable<CourseDTO> GetCourses(string order, string teacherId, int subjectId)
        {
            var courses = _database.CourseRepository.GetAll().Where(x => x.IsDeleted == false).ToList();
            var coursesDto = _mapper.Map<List<CourseDTO>>(courses);
            switch (order)
            {
                case "desc":
                    coursesDto = coursesDto.OrderByDescending(x => x.Title).ToList();
                    break;
                case "dur":
                    coursesDto = coursesDto.OrderBy(x => x.Durations).ToList();
                    break;
                case "number":
                    coursesDto = coursesDto.OrderBy(x => x.CourseStudents.Count).ToList();
                    break;
                case "asc":
                default:
                    coursesDto = coursesDto.OrderBy(x => x.Title).ToList();
                    break;
            }
            if (teacherId != "all")
            {
                coursesDto = coursesDto.Where(x => x.Teacher.Id == teacherId).ToList();
            }
            if (subjectId != 0)
            {
                coursesDto = coursesDto.Where(x => x.Subject.Id == subjectId).ToList();
            }
            for (var i = 0; i < coursesDto.Count; i++)
            {
                if (coursesDto[i].Description.Length > 300)
                {
                    coursesDto[i].Description = coursesDto[i].Description.Substring(0, 300) + "...";
                }
            }
            return coursesDto;
        }

        public CourseDTO GetCourseDtoById(int id)
        {
            var course = _database.CourseRepository.Get(id);
            var courseDto = _mapper.Map<CourseDTO>(course);
            return courseDto;
        }

        public void EditCourse(CourseDTO courseDto)
        {
            if (courseDto.CourseStatus == 1)
            {
                courseDto.IsStarted = false;
                courseDto.IsFinished = false;
            }
            else if (courseDto.CourseStatus == 2)
            {
                courseDto.IsStarted = true;
                courseDto.IsFinished = false;
            }
            else
            {
                courseDto.IsStarted = true;
                courseDto.IsFinished = true;
            }
            var course = _mapper.Map<Course>(courseDto);
            _database.CourseRepository.Update(course);
            _database.Save();
        }

        public void DeleteCourse(int id)
        {
            _database.CourseRepository.Delete(id);
            _database.Save();
        }

        public string GetCourseStatus(int id)
        {
            var course = _database.CourseRepository.Get(id);
            if (course.IsStarted && course.IsFinished == false)
            {
                return "started";
            }
            if (course.IsFinished)
            {
                return "finished";
            }
            if (course.IsStarted == false && course.IsFinished == false)
            {
                return "not started";
            }

            return "";
        }

        public IEnumerable<SelectListItem> GetParametersAsListForFilter()
        {
            List<SelectListItem> sortParams = new List<SelectListItem>();
            sortParams.Add(new SelectListItem()
            {
                Value = OrderDistributor.GetValueAscending(),
                Text = OrderDistributor.GetTextAscending()
            });
            sortParams.Add(new SelectListItem()
            {
                Value = OrderDistributor.GetValueDescending(),
                Text = OrderDistributor.GetTextDescending()
            });
            sortParams.Add(new SelectListItem()
            {
                Value = OrderDistributor.GetValueDuration(),
                Text = OrderDistributor.GetTextDuration()
            });
            sortParams.Add(new SelectListItem()
            {
                Value = OrderDistributor.GetValueNumberOfStudents(),
                Text = OrderDistributor.GetTextNumberOfStudents()
            });
            return sortParams;
        }

        public void ChangeCourseStatus(int id)
        {
            var course = _database.CourseRepository.Get(id);
            if (course.IsStarted == false && course.IsFinished == false)
            {
                course.IsStarted = true;
            }
            else if(course.IsStarted == true && course.IsFinished == false)
            {
                course.IsFinished = true;
            }
            _database.CourseRepository.Update(course);
            _database.Save();
        }

        public void Subscribe(string userId, int courseId)
        {
            var student = _database.StudentRepository.GetByStringId(userId);
            student.CourseStudents.Add(new CourseStudent() { StudentId = userId, CourseId = courseId});
            _database.StudentRepository.Update(student);
            _database.Save();
        }

        public IEnumerable<CourseDTO> CheckSubscribe(string userId, IEnumerable<CourseDTO> coursesDto)
        {
            var student = _database.StudentRepository.GetByStringId(userId);
            foreach (var courseStudent in student.CourseStudents)
            {
                foreach (var course in coursesDto)
                {
                    if (courseStudent.CourseId == course.Id && courseStudent.StudentId == userId)
                    {
                        course.IsSubscribed = true;
                    }
                }
            }
            return coursesDto;
        }

        public IEnumerable<CourseDTO> GetFinishedCourses(string userId)
        {
            var courseStudents = _database.CourseStudentRepository.Find(x => x.StudentId == userId).ToList();
            List<int> coursesId = new List<int>();
            foreach (var courseStudent in courseStudents)
            {
                coursesId.Add(courseStudent.CourseId);
            }
            var courses = _database.CourseRepository
                .Find(x => x.IsDeleted == false && x.IsStarted == true && x.IsFinished == true).ToList();
            List<Course> coursesResult = new List<Course>();
            foreach (var course in courses)
            {
                foreach (var courseId in coursesId)
                {
                    if (course.Id == courseId)
                    {
                        coursesResult.Add(course);
                    }
                }
            }
            var coursesDto = _mapper.Map<List<CourseDTO>>(coursesResult);
            foreach (CourseDTO courseDto in coursesDto)
            {
                foreach (var courseStudent in courseStudents)
                {
                    if (courseDto.Id == courseStudent.CourseId)
                    {
                        courseDto.Mark = courseStudent.Mark;
                    }
                }
                if (courseDto.Description.Length > 100)
                {
                    courseDto.Description = courseDto.Description.Substring(0, 100) + "...";
                }
            }
            return coursesDto;
        }

        public IEnumerable<CourseDTO> GetInProcessCourses(string userId)
        {
            var courseStudents = _database.CourseStudentRepository.Find(x => x.StudentId == userId).ToList();
            List<int> coursesId = new List<int>();
            foreach (var courseStudent in courseStudents)
            {
                coursesId.Add(courseStudent.CourseId);
            }
            var courses = _database.CourseRepository
                .Find(x => x.IsDeleted == false && x.IsStarted == true && x.IsFinished == false).ToList();
            List<Course> coursesResult = new List<Course>();
            foreach (var course in courses)
            {
                foreach (var courseId in coursesId)
                {
                    if (course.Id == courseId)
                    {
                        coursesResult.Add(course);
                    }
                }
            }
            var coursesDto = _mapper.Map<List<CourseDTO>>(coursesResult);
            foreach (CourseDTO courseDto in coursesDto)
            {
                if (courseDto.Description.Length > 100)
                {
                    courseDto.Description = courseDto.Description.Substring(0, 100) + "...";
                }
            }
            return coursesDto;
        }

        public IEnumerable<CourseDTO> GetNotStartedCourses(string userId)
        {
            var courseStudents = _database.CourseStudentRepository.Find(x => x.StudentId == userId).ToList();
            List<int> coursesId = new List<int>();
            foreach (var courseStudent in courseStudents)
            {
                coursesId.Add(courseStudent.CourseId);
            }
            var courses = _database.CourseRepository
                .Find(x => x.IsDeleted == false && x.IsStarted == false && x.IsFinished == false).ToList();
            List<Course> coursesResult = new List<Course>();
            foreach (var course in courses)
            {
                foreach (var courseId in coursesId)
                {
                    if (course.Id == courseId)
                    {
                        coursesResult.Add(course);
                    }
                }
            }
            var coursesDto = _mapper.Map<List<CourseDTO>>(coursesResult);
            foreach (CourseDTO courseDto in coursesDto)
            {
                if (courseDto.Description.Length > 100)
                {
                    courseDto.Description = courseDto.Description.Substring(0, 100) + "...";
                }
            }
            return coursesDto;
        }

        public IEnumerable<CourseDTO> GetNotStartedCourses(IEnumerable<CourseDTO> coursesDto)
        {
            return coursesDto.Where(x => x.IsStarted == false).ToList();
        }

        public IEnumerable<CourseDTO> GetTeacherCourses(string teacherId)
        {
            var allCourses = _database.CourseRepository.GetAll();
            var courses = allCourses.Where(x=> x.IsDeleted == false && x.Teacher.Id == teacherId);
            var coursesDto = _mapper.Map<List<CourseDTO>>(courses);
            return coursesDto;
        }

        public IEnumerable<CourseDTO> GetTeacherIsUpdatedCourses(string teacherId)
        {
            var allCourses = _database.CourseRepository.GetAll();
            var isUpdatedCourses = allCourses.Where(x =>  x.IsDeleted == false && x.Teacher.Id == teacherId);
            var isUpdatedCoursesDto = _mapper.Map<List<CourseDTO>>(isUpdatedCourses);
            return isUpdatedCoursesDto;
        }

        public IEnumerable<CourseDTO> GetTeacherNotUpdatedCourses(string teacherId)
        {
            var allCourses = _database.CourseRepository.GetAll();
            var notUpdatedCourses = allCourses.Where(x => x.IsUpdated == false && x.IsFinished && x.IsDeleted == false && x.Teacher.Id == teacherId);
            var notUpdatedCoursesDto = _mapper.Map<List<CourseDTO>>(notUpdatedCourses);
            return notUpdatedCoursesDto;
        }

        public IEnumerable<SelectListItem> GetCourseStatusAsSelectList()
        {
            List<SelectListItem> courseStatus = new List<SelectListItem>();
            courseStatus.Add(new SelectListItem()
            {
                Value = "1",
                Text = "not started"
            });
            courseStatus.Add(new SelectListItem()
            {
                Value = "2",
                Text = "started"
            });
            courseStatus.Add(new SelectListItem()
            {
                Value = "3",
                Text = "finished"
            });

            return courseStatus;
        }
    }
}