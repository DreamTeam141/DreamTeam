using System.ComponentModel.DataAnnotations;

namespace Faculty.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}