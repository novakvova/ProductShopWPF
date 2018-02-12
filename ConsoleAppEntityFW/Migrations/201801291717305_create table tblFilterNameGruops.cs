namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtabletblFilterNameGruops : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilterNameGruops",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValieId })
                .ForeignKey("dbo.tblFilterNames", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValues", t => t.FilterValieId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilterNameGruops", "FilterValieId", "dbo.tblFilterValues");
            DropForeignKey("dbo.tblFilterNameGruops", "FilterNameId", "dbo.tblFilterNames");
            DropIndex("dbo.tblFilterNameGruops", new[] { "FilterValieId" });
            DropIndex("dbo.tblFilterNameGruops", new[] { "FilterNameId" });
            DropTable("dbo.tblFilterNameGruops");
        }
    }
}
