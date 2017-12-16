using System.Collections.Generic;

namespace Faculty.Models
{
    public class JournalViewModel
    {
        public int CourseId { get; set; }
        public List<CourseStudentViewModel> CourseStudentViewModels { get; set; }
    }
}