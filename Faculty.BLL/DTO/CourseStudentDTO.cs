namespace Faculty.BLL.DTO
{
    public class CourseStudentDTO
    {
        public int CourseId { get; set; }
        public string StudentId { get; set; }

        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
        public int Mark { get; set; }

        public int Set { get; set; }
    }
}