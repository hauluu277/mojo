namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Col_IsShow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bentre_BieuMauThongTin", "IsShow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.bentre_BieuMauThongTin", "IsShow");
        }
    }
}
