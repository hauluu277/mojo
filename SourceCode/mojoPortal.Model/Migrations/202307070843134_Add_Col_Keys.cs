namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Col_Keys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.bentre_BieuMauThongTin", "Keys", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.bentre_BieuMauThongTin", "Keys");
        }
    }
}
