using System.Collections.Generic;

namespace Faculty.Models
{
    public class JournalIndexViewModel
    {
        public List<CourseViewModel> NotUpdatedCourses { get; set; }
        public List<CourseViewModel> IsUpdatedCoursesViewModels { get; set; }
    }
}