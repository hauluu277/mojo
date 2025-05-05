namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_bentreTieuChiBieuMau : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bentre_TieuChiBieuMau",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Ten = c.String(),
                        Key = c.String(),
                        IdBieuMau = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bentre_TieuChiBieuMau");
        }
    }
}
