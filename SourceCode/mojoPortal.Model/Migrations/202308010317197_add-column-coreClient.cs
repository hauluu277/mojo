namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumncoreClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_Client", "TenDangNhap", c => c.String());
            AddColumn("dbo.core_Client", "MatKhau", c => c.String());
            AddColumn("dbo.core_Client", "ThoiGianLayTin", c => c.Int());
            AddColumn("dbo.core_Client", "isLayTinTuDong", c => c.Boolean());
            AddColumn("dbo.core_Client", "APIChuyenMucTin", c => c.String());
            AddColumn("dbo.core_Client", "APIDanhSachTin", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_Client", "APIDanhSachTin");
            DropColumn("dbo.core_Client", "APIChuyenMucTin");
            DropColumn("dbo.core_Client", "isLayTinTuDong");
            DropColumn("dbo.core_Client", "ThoiGianLayTin");
            DropColumn("dbo.core_Client", "MatKhau");
            DropColumn("dbo.core_Client", "TenDangNhap");
        }
    }
}
