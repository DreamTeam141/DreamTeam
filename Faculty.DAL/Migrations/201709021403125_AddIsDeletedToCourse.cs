namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "IsDeleted");
        }
    }
}
