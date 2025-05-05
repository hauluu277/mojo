namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bentre_KeKhaiBieuMau",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        IdNopBieuMau = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.bentre_NopBieuMau",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hoten = c.String(),
                        Email = c.String(),
                        DienThoai = c.String(),
                        DiaChi = c.String(),
                        IdBieuMauThongTin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bentre_NopBieuMau");
            DropTable("dbo.bentre_KeKhaiBieuMau");
        }
    }
}
