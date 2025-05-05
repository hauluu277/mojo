namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createCauHinh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.core_CauHinhHienThiLog",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        TruongHienThi = c.String(),
                        CreateBy = c.String(),
                        CreateByUser = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        EditByUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.core_CauHinhHienThiLog");
        }
    }
}
