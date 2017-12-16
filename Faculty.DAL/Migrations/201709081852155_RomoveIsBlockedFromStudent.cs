namespace Faculty.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RomoveIsBlockedFromStudent : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "IsBlocked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "IsBlocked", c => c.Boolean(nullable: false));
        }
    }
}
