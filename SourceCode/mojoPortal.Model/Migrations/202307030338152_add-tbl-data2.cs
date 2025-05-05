namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbldata2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbldata",
                c => new
                    {
                        ItemID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            AlterColumn("dbo.mp_Roles", "SiteID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.mp_Roles", "SiteID", c => c.Int(nullable: false));
            DropTable("dbo.tbldata");
        }
    }
}
