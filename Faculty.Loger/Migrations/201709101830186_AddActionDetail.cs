namespace Faculty.Logger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        UserId = c.String(),
                        UserName = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionDetails");
        }
    }
}
