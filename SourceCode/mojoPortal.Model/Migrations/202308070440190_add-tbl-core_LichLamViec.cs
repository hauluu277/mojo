namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblcore_LichLamViec : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.core_LichLamViec",
            //    c => new
            //        {
            //            ItemID = c.Int(nullable: false, identity: true),
            //            NgayLamViec = c.DateTime(),
            //            ThoiGianLamViec = c.DateTime(),
            //            NoiDung = c.String(),
            //            DiaDiem = c.String(),
            //            ThanhPhanThamDu = c.String(),
            //            CreatedDate = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedByUser = c.Int(),
            //            EditedBy = c.Int(),
            //            EditedDate = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.ItemID);
            
            //CreateTable(
            //    "dbo.mp_ContactFormMessage",
            //    c => new
            //        {
            //            RowGuid = c.Guid(nullable: false),
            //            SiteGuid = c.Guid(nullable: false),
            //            ModuleGuid = c.Guid(nullable: false),
            //            Email = c.String(),
            //            Url = c.String(),
            //            Subject = c.String(),
            //            Message = c.String(),
            //            CreatedUtc = c.DateTime(nullable: false),
            //            CreatedFromIpAddress = c.String(),
            //            UserGuid = c.Guid(nullable: false),
            //            TrangThai = c.Int(nullable: false),
            //            ThoiGianPhanHoi = c.DateTime(),
            //            NoiDungPhanHoi = c.String(),
            //        })
            //    .PrimaryKey(t => t.RowGuid);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.mp_ContactFormMessage");
            //DropTable("dbo.core_LichLamViec");
        }
    }
}
