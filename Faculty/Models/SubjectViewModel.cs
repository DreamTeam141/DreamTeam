using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class SubjectViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}