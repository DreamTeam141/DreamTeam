using System.Web.Mvc;

namespace Faculty.Models
{
    public class CourseDetailsViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Durations { get; set; }
        public string CourseStatus { get; set; }

        public string Teacher { get; set; }
        public string Subject { get; set; }
    }
}