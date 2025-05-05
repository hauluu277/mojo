namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetblcore_LichLamViec : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.core_LichLamViec", "IsPublish", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.core_LichLamViec", "IsPublish");
        }
    }
}
