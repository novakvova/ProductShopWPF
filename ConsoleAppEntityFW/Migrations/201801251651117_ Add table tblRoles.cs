namespace ConsoleAppEntityFW.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtabletblRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUserProfiles",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        UserProfile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.UserProfile_Id })
                .ForeignKey("dbo.tblRoles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleUserProfiles", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.RoleUserProfiles", "Role_Id", "dbo.tblRoles");
            DropIndex("dbo.RoleUserProfiles", new[] { "UserProfile_Id" });
            DropIndex("dbo.RoleUserProfiles", new[] { "Role_Id" });
            DropTable("dbo.RoleUserProfiles");
            DropTable("dbo.tblRoles");
        }
    }
}
