namespace mojoPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablemd_LichCongTacNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.md_LichCongTacNew",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        SideID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        PageID = c.Int(nullable: false),
                        StartTime = c.String(),
                        EndTime = c.String(),
                        Summary = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Week = c.Int(),
                        DateCreate = c.DateTime(),
                        CreateBy = c.Int(),
                        StartWeek = c.DateTime(),
                        EndWeed = c.DateTime(),
                        Nam = c.Int(),
                        DayID = c.Int(),
                        Thu = c.String(),
                        ThoiGian = c.String(),
                        NoiDung = c.String(),
                        DiaDiem = c.String(),
                        ThanhPhanThamDu = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            

        }
        
        public override void Down()
        {

            DropTable("dbo.md_LichCongTacNew");
        }
    }
}
