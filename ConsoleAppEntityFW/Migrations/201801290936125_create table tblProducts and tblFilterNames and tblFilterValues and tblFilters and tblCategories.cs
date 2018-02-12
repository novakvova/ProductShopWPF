namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtabletblProductsandtblFilterNamesandtblFilterValuesandtblFiltersandtblCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 250),
                        IsHead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.tblProducts", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblProducts", "CategoryId");
            AddForeignKey("dbo.tblProducts", "CategoryId", "dbo.tblCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProducts", "CategoryId", "dbo.tblCategories");
            DropForeignKey("dbo.tblCategories", "ParentId", "dbo.tblCategories");
            DropIndex("dbo.tblProducts", new[] { "CategoryId" });
            DropIndex("dbo.tblCategories", new[] { "ParentId" });
            DropColumn("dbo.tblProducts", "CategoryId");
            DropTable("dbo.tblCategories");
        }
    }
}
