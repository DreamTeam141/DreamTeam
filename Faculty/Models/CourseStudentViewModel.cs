using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class CourseStudentViewModel
    {
        [HiddenInput]
        public int CourseId { get; set; }
        [HiddenInput]
        public string StudentId { get; set; }

        public CourseViewModel Course { get; set; }
        public StudentViewModel Student { get; set; }

        [Required(ErrorMessage = "Mark is required")]
        [Range(1, 5, ErrorMessage = "From 1 to 5")]
        public int Mark { get; set; }
    }
}