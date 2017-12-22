namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedSetToCourseStudentandCurrentSetToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CurrentSet", c => c.Int(nullable: false));
            AddColumn("dbo.CourseStudents", "Set", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseStudents", "Set");
            DropColumn("dbo.Courses", "CurrentSet");
        }
    }
}
