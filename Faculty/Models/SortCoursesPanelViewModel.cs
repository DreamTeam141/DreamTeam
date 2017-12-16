using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class SortCoursesPanelViewModel
    {
        [HiddenInput]
        public string Order { get; set; }
        public List<SelectListItem> OrderParams { get; set; }

        [HiddenInput]
        [Display(Name = "Teacher")]
        public string TeacherSortId { get; set; }
        public List<SelectListItem> TeacherSortParams { get; set; }

        [HiddenInput]
        [Display(Name = "Subject")]
        public int SubjectSortId { get; set; }
        public List<SelectListItem> SubjectSortParams { get; set; }
    }
}