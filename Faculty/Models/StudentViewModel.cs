using System.Collections.Generic;

namespace Faculty.Models
{
    public class StudentViewModel
    {
        public string Id { get; set; }
        public UserViewModel ApplicationUser { get; set; }
        public ICollection<CourseStudentViewModel> CourseStudents { get; set; }

    }
}