namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Text = c.String(maxLength: 1000),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        Password = c.String(maxLength: 512),
                        Salt = c.Binary(),
                        FullName = c.String(maxLength: 100),
                        Birthday = c.DateTime(),
                        Sex = c.Int(nullable: false),
                        Description = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.UserLikes",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        OtherUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.OtherUserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.OtherUserId)
                .Index(t => t.UserId)
                .Index(t => t.OtherUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLikes", "OtherUserId", "dbo.Users");
            DropForeignKey("dbo.UserLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.UserLikes", new[] { "OtherUserId" });
            DropIndex("dbo.UserLikes", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropTable("dbo.UserLikes");
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Cities");
        }
    }
}
