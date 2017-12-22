using Faculty.BLL.DTO;

namespace Faculty.BLL.Interfaces
{
    public interface IJournalService
    {
        void UpdateCourseStudent(CourseStudentDTO item);
        void Delete(CourseStudentDTO item);
        CourseStudentDTO GetCourseStudent(string userId, int courseId);
    }
}