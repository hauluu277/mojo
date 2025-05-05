namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Col_NgayNop_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bentre_NopBieuMau", "NgayNop", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bentre_NopBieuMau", "NgayNop");
        }
    }
}
