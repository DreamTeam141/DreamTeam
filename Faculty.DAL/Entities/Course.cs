using System.Collections.Generic;

namespace Faculty.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Durations { get; set; }
        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }

        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }

        public byte[] Photo { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual Subject Subject { get; set; }
    }
}