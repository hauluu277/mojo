namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetblartile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.md_Articles", "IsCongThanhVien", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.md_Articles", "IsCongThanhVien");
        }
    }
}
