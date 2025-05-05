namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblcore_DanhMuc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.core_DanhMuc",
                c => new
                    {
                        ItemID = c.Long(nullable: false, identity: true),
                        SiteID = c.Int(nullable: false),
                        Title = c.String(),
                        UrlLink = c.String(),
                        PathIMG = c.String(),
                        Sapo = c.String(),
                        OrderBy = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedByUser = c.Int(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            //AddColumn("dbo.core_Category", "ShowCategoryChild", c => c.Boolean());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.core_Category", "ShowCategoryChild");
            DropTable("dbo.core_DanhMuc");
        }
    }
}
