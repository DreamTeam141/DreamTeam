using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Second Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Patronymic is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Required at least one digit and one letter")]
        [MinLength(6, ErrorMessage = "Min length 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        [RegularExpression(@"^(380)\d{9}$", ErrorMessage = "380yyxxxxxxx")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
    }
}