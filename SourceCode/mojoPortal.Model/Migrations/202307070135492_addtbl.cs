namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtbl : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.core_GiaoDien",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            SiteID = c.Int(),
            //            TenGiaoDien = c.String(),
            //            MaGiaoDien = c.String(),
            //            DuongDan = c.String(),
            //            DuongDanZipTaiLen = c.String(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            CreatedDate = c.DateTime(),
            //            EditDate = c.DateTime(),
            //            EditByUser = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.core_LogHeThong",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        DiaChiIP = c.String(),
                        Type = c.String(),
                        UrlThaoTac = c.String(),
                        HanhDongThaoTac = c.String(),
                        NoiDung = c.String(),
                        DuongDanThaoTac = c.String(),
                        CreatedBy = c.String(),
                        CreatedByUser = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        EditByUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.core_LogHeThong");
            //DropTable("dbo.core_GiaoDien");
        }
    }
}
