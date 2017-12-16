using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faculty.DAL.Entities
{
    public class CourseStudent
    {
        [Key, Column(Order = 0)]
        public int CourseId { get; set; }
        [Key, Column(Order = 1)]
        public string StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        public int Mark { get; set; }
    }
}