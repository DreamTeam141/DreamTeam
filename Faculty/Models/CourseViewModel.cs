using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class 
        CourseViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Durations is required")]
        [Range(1, 100)]
        public int Durations { get; set; }
        
        public List<SelectListItem> Subject { get; set; }
        
        [HiddenInput]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        public List<SelectListItem> Teachers { get; set; }

        [HiddenInput]
        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }

        public List<SelectListItem> CourseStatus { get; set; }

        [Display(Name = "Course Status")]
        public int CourseStatusId { get; set; }

        [HiddenInput]
        public bool IsSubscribed { get; set; }

        [Required(ErrorMessage = "Mark is required")]
        public int Mark { get; set; }

        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }

        public IEnumerable<CourseStudentViewModel> CourseStudents { get; set; }

        public byte[] Photo { get; set; }
    }
}
