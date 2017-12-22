namespace Faculty.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSetAsKeyToCoursestudent : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CourseStudents");
            AddPrimaryKey("dbo.CourseStudents", new[] { "CourseId", "StudentId", "Set" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CourseStudents");
            AddPrimaryKey("dbo.CourseStudents", new[] { "CourseId", "StudentId" });
        }
    }
}
