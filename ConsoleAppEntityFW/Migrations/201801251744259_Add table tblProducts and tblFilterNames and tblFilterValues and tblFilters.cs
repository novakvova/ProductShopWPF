namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtabletblProductsandtblFilterNamesandtblFilterValuesandtblFilters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilterNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblFilters",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValieId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValieId, t.ProductId })
                .ForeignKey("dbo.tblFilterNames", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValues", t => t.FilterValieId, cascadeDelete: true)
                .ForeignKey("dbo.tblProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValieId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tblFilterValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilters", "ProductId", "dbo.tblProducts");
            DropForeignKey("dbo.tblFilters", "FilterValieId", "dbo.tblFilterValues");
            DropForeignKey("dbo.tblFilters", "FilterNameId", "dbo.tblFilterNames");
            DropIndex("dbo.tblFilters", new[] { "ProductId" });
            DropIndex("dbo.tblFilters", new[] { "FilterValieId" });
            DropIndex("dbo.tblFilters", new[] { "FilterNameId" });
            DropTable("dbo.tblProducts");
            DropTable("dbo.tblFilterValues");
            DropTable("dbo.tblFilters");
            DropTable("dbo.tblFilterNames");
        }
    }
}
