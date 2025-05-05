namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetblthutucbieumau : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_ThuTuc_BieuMau", "TotalDownload", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_ThuTuc_BieuMau", "TotalDownload");
        }
    }
}
