namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblcore_SettingSSO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.core_SettingSSO",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        UrlSSO = c.String(),
                        UrlSSOReturn = c.String(),
                        ActiveSSO = c.Boolean(nullable: false),
                        BackgroundButton = c.String(),
                        TypeTheme = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            //DropColumn("dbo.core_LogHeThong", "UrlThaoTac");
            //DropColumn("dbo.core_LogHeThong", "EditDate");
            //DropColumn("dbo.core_LogHeThong", "EditByUser");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.core_LogHeThong", "EditByUser", c => c.Int(nullable: false));
            //AddColumn("dbo.core_LogHeThong", "EditDate", c => c.DateTime(nullable: false));
            //AddColumn("dbo.core_LogHeThong", "UrlThaoTac", c => c.String());
            DropTable("dbo.core_SettingSSO");
        }
    }
}
