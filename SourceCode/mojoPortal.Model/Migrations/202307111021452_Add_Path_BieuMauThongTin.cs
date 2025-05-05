namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Path_BieuMauThongTin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bentre_BieuMauThongTin", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bentre_BieuMauThongTin", "Path");
        }
    }
}
