namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addremovetbldata : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.tbldata");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tbldata",
                c => new
                    {
                        ItemID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
    }
}
