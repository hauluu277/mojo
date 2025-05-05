namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addChuyenMucIdInTableCore_client : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_Client", "ChuyenMucId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_Client", "ChuyenMucId");
        }
    }
}
