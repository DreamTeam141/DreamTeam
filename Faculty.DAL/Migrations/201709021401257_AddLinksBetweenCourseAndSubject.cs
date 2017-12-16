namespace Faculty.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLinksBetweenCourseAndSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Subject_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Subject_Id");
            AddForeignKey("dbo.Courses", "Subject_Id", "dbo.Subjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Courses", new[] { "Subject_Id" });
            DropColumn("dbo.Courses", "Subject_Id");
        }
    }
}
