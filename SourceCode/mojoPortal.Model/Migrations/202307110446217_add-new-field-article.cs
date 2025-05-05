namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewfieldarticle : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.md_ArticleCategory",
            //    c => new
            //        {
            //            ItemID = c.Long(nullable: false, identity: true),
            //            CategoryID = c.Int(),
            //            SiteID = c.Int(),
            //            ArticleID = c.Long(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            AddColumn("dbo.md_Articles", "ViTriHienThiNgayDang", c => c.String());
            AddColumn("dbo.md_Articles", "IsHienThiTacGia", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.md_Articles", "IsHienThiTacGia");
            DropColumn("dbo.md_Articles", "ViTriHienThiNgayDang");
            //DropTable("dbo.md_ArticleCategory");
        }
    }
}
