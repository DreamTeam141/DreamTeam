namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addExceptionDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionDetails");
        }
    }
}
