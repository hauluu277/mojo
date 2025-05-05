namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetblcore_SettingSSO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_SettingSSO", "IsDisable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_SettingSSO", "IsDisable");
        }
    }
}
