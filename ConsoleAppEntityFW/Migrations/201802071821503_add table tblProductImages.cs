namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtabletblProductImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblProductImages", "ProductId", "dbo.tblProducts");
            DropIndex("dbo.tblProductImages", new[] { "ProductId" });
            DropTable("dbo.tblProductImages");
        }
    }
}
