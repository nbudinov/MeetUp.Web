namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Size = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "UserId", "dbo.Users");
            DropIndex("dbo.Images", new[] { "UserId" });
            DropTable("dbo.Images");
        }
    }
}
