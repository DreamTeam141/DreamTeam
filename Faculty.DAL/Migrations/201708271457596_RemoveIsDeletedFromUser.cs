namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsDeletedFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
