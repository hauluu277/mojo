namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetblcore_ThongKeTruyCap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_ThongKeTruyCap", "CurrentMonth", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_ThongKeTruyCap", "CurrentMonth");
        }
    }
}
