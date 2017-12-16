using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Faculty.Models
{
    public class UserViewModel
    {
        [HiddenInput]
        public string Id { get; set; }
        
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

      
    }
}