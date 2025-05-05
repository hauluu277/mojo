namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_BenTreBieuMauThongTin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bentre_BieuMauThongTin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        NoiDungHTML = c.String(),
                        NgayTao = c.DateTime(),
                        NgayCapNhat = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bentre_BieuMauThongTin");
        }
    }
}
