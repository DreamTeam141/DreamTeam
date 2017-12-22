namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addIsUpdatedToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsUpdated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "IsUpdated");
        }
    }
}
