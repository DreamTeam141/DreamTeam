namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Photo");
        }
    }
}
