using System.Collections.Generic;
using System.Web.Mvc;

namespace Faculty.BLL.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Durations { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }

        public bool IsUpdated { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsSubscribed { get; set; }

        public int Mark { get; set; }
        public byte[] Photo { get; set; }

        public int CourseStatus { get; set; }

        public TeacherDTO Teacher { get; set; }
        public ICollection<CourseStudentDTO> CourseStudents { get; set; }
        public SubjectDTO Subject { get; set; }
    }
}