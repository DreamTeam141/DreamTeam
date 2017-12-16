using System.Collections.Generic;

namespace Faculty.BLL.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<CourseDTO> Courses { get; set; }
    }
}