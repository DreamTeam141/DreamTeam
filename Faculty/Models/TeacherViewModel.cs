using System.Collections.Generic;

namespace Faculty.Models
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
        public UserViewModel ApplicationUser { get; set; }
        public ICollection<CourseViewModel> Courses { get; set; }
    }
}