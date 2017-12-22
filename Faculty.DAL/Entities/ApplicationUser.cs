using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Faculty.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }
     }
}