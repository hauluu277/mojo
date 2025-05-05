namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addClientIdInTablemd_Articles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.md_Articles", "ClientId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.md_Articles", "ClientId");
        }
    }
}
