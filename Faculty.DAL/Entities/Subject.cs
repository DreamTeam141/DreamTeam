using System.Collections.Generic;

namespace Faculty.DAL.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}