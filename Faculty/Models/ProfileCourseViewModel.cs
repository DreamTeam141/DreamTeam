using System.Collections.Generic;

namespace Faculty.Models
{
    public class ProfileCourseViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public IEnumerable<CourseViewModel> NotStartedCourses { get; set; }
        public IEnumerable<CourseViewModel> InProcessCourses { get; set; }
        public IEnumerable<CourseViewModel> FinishedCourses { get; set; }
    }
}