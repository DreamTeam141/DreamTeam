using System.Data.Entity;
using Faculty.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Faculty.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("IdentityDb", throwIfV1Schema: false)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasRequired(c => c.ApplicationUser)
                .WithOptional(c => c.Student);

            modelBuilder.Entity<Teacher>()
                .HasRequired(c => c.ApplicationUser)
                .WithOptional(c => c.Teacher);
        }
    }
}