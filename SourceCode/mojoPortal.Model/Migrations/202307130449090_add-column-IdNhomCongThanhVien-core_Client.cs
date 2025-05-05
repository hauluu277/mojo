namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnIdNhomCongThanhViencore_Client : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.core_Client", "IdNhomCongThanhVien", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.core_Client", "IdNhomCongThanhVien");
        }
    }
}
