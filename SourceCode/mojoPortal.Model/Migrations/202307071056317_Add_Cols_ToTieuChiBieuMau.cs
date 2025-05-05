namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Cols_ToTieuChiBieuMau : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bentre_TieuChiBieuMau", "Required", c => c.Boolean(nullable: false));
            AddColumn("dbo.bentre_TieuChiBieuMau", "IsComboBox", c => c.Boolean(nullable: false));
            AddColumn("dbo.bentre_TieuChiBieuMau", "SoThuTu", c => c.Int());
            AddColumn("dbo.bentre_TieuChiBieuMau", "DataType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bentre_TieuChiBieuMau", "DataType");
            DropColumn("dbo.bentre_TieuChiBieuMau", "SoThuTu");
            DropColumn("dbo.bentre_TieuChiBieuMau", "IsComboBox");
            DropColumn("dbo.bentre_TieuChiBieuMau", "Required");
        }
    }
}
