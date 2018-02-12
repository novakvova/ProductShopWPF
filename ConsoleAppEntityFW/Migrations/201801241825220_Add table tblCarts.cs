namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtabletblCarts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCarts",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId)
                .Index(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCarts", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.tblCarts", new[] { "UserProfileId" });
            DropTable("dbo.tblCarts");
        }
    }
}
