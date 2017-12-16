using System.Collections.Generic;

namespace Faculty.BLL.DTO
{
    public class StudentDTO
    {
        public string Id { get; set; }
        public UserDTO ApplicationUser { get; set; }
        public ICollection<CourseStudentDTO> CourseStudents { get; set; }

        public int Mark { get; set; }
    }
}