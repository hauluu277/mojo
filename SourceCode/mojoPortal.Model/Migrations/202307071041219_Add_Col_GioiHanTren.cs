namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Col_GioiHanTren : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bentre_TieuChiBieuMau", "GioiHanTren", c => c.Double());
            AddColumn("dbo.bentre_TieuChiBieuMau", "GioiHanDuoi", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bentre_TieuChiBieuMau", "GioiHanDuoi");
            DropColumn("dbo.bentre_TieuChiBieuMau", "GioiHanTren");
        }
    }
}
