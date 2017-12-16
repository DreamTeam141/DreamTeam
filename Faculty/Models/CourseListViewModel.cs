using System.Collections.Generic;

namespace Faculty.Models
{
    public class CourseListViewModel
    {
        public CourseViewModel Course { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public SortCoursesPanelViewModel PanelViewModel { get; set; }
    }
}