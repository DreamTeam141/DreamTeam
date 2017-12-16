using System.Data.Entity;
using Faculty.Logger.Entities;

namespace Faculty.Logger.EF
{
    public class ExceptionDetailContext : DbContext
    {
        public ExceptionDetailContext() : base ("ExceptionDb")
        {
            
        }

        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
        public DbSet<ActionDetail> ActionDetails { get; set; }
    }
}