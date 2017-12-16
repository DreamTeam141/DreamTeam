using System.Collections.Generic;

namespace Faculty.BLL.DTO
{
    public class TeacherDTO
    {
        public string Id { get; set; }
        public UserDTO ApplicationUser { get; set; }
        public ICollection<CourseDTO> Courses { get; set; }
    }
}