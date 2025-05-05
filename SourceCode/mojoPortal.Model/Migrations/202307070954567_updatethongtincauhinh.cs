namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatethongtincauhinh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_CauHinhHienThiLog", "MaTruongHienThi", c => c.String());
            AddColumn("dbo.core_CauHinhHienThiLog", "IsShow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_CauHinhHienThiLog", "IsShow");
            DropColumn("dbo.core_CauHinhHienThiLog", "MaTruongHienThi");
        }
    }
}
