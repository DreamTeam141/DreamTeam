using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faculty.DAL.Entities
{
    public class Student
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
