namespace Faculty.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExceptionDetails : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ExceptionDetails");
        }
        
        public override void Down()
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
    }
}
