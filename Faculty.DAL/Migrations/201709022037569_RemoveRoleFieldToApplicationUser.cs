namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRoleFieldToApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
        }
    }
}
