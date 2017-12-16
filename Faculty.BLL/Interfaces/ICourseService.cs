using System.Collections.Generic;
using System.Web.Mvc;
using Faculty.BLL.DTO;

namespace Faculty.BLL.Interfaces
{
    public interface ICourseService
    {
        void Create(CourseDTO courseDto);
        IEnumerable<CourseDTO> GetCourses(string order, string teacherId, int subjectId);
        CourseDTO GetCourseDtoById(int id);
        void EditCourse(CourseDTO courseDto);
        void DeleteCourse(int id);
        string GetCourseStatus(int id);
        IEnumerable<SelectListItem> GetParametersAsListForFilter();
        IEnumerable<SelectListItem> GetCourseStatusAsSelectList();
        void ChangeCourseStatus(int id);
        void Subscribe(string userId, int courseId);
        IEnumerable<CourseDTO> CheckSubscribe(string userId, IEnumerable<CourseDTO> coursesDto);
        IEnumerable<CourseDTO> GetFinishedCourses(string userId);
        IEnumerable<CourseDTO> GetInProcessCourses(string userId);
        IEnumerable<CourseDTO> GetNotStartedCourses(string userId);
        IEnumerable<CourseDTO> GetNotStartedCourses(IEnumerable<CourseDTO> coursesDto);
        IEnumerable<CourseDTO> GetTeacherCourses(string teacherId);
        IEnumerable<CourseDTO> GetTeacherIsUpdatedCourses(string teacherId);
        IEnumerable<CourseDTO> GetTeacherNotUpdatedCourses(string teacherId);

    }
}
