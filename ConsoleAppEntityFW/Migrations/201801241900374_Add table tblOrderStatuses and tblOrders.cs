namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtabletblOrderStatusesandtblOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                        UserProfileId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblOrderStatuses", t => t.OrderStatusId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId)
                .Index(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.tblOrderStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblOrders", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.tblOrders", "OrderStatusId", "dbo.tblOrderStatuses");
            DropIndex("dbo.tblOrders", new[] { "OrderStatusId" });
            DropIndex("dbo.tblOrders", new[] { "UserProfileId" });
            DropTable("dbo.tblOrderStatuses");
            DropTable("dbo.tblOrders");
        }
    }
}
