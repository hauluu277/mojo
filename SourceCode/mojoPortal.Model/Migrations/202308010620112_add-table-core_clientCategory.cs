namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablecore_clientCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.core_ClientCategory",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        CategoryClientId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.core_ClientCategory");
        }
    }
}
