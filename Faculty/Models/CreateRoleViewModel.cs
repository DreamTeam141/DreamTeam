using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}