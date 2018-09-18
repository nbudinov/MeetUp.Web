namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DailySuggestionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
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
                        Role = c.Int(nullable: false),
                        FullName = c.String(maxLength: 100),
                        Birthday = c.DateTime(),
                        CreateTime = c.DateTime(),
                        LastOnline = c.DateTime(),
                        Location = c.String(),
                        Sex = c.Int(nullable: false),
                        Description = c.String(),
                        Active = c.Int(nullable: false),
                        Deleted = c.Int(nullable: false),
                        Banned = c.Int(nullable: false),
                        CityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Extension = c.String(),
                        Size = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                "dbo.UserSuperLikeLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserLikingId = c.Int(),
                        UserLikedId = c.Int(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserLikedId)
                .ForeignKey("dbo.Users", t => t.UserLikingId)
                .Index(t => t.UserLikingId)
                .Index(t => t.UserLikedId);
            
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
            
            CreateTable(
                "dbo.UserSuperLikes",
                c => new
                    {
                        LikingUserId = c.Int(nullable: false),
                        LikedUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikingUserId, t.LikedUserId })
                .ForeignKey("dbo.Users", t => t.LikingUserId)
                .ForeignKey("dbo.Users", t => t.LikedUserId)
                .Index(t => t.LikingUserId)
                .Index(t => t.LikedUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSuperLikeLogs", "UserLikingId", "dbo.Users");
            DropForeignKey("dbo.UserSuperLikeLogs", "UserLikedId", "dbo.Users");
            DropForeignKey("dbo.DailySuggestionLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserSuperLikes", "LikedUserId", "dbo.Users");
            DropForeignKey("dbo.UserSuperLikes", "LikingUserId", "dbo.Users");
            DropForeignKey("dbo.UserLikes", "OtherUserId", "dbo.Users");
            DropForeignKey("dbo.UserLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Images", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.UserSuperLikes", new[] { "LikedUserId" });
            DropIndex("dbo.UserSuperLikes", new[] { "LikingUserId" });
            DropIndex("dbo.UserLikes", new[] { "OtherUserId" });
            DropIndex("dbo.UserLikes", new[] { "UserId" });
            DropIndex("dbo.UserSuperLikeLogs", new[] { "UserLikedId" });
            DropIndex("dbo.UserSuperLikeLogs", new[] { "UserLikingId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Images", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.DailySuggestionLogs", new[] { "UserId" });
            DropTable("dbo.UserSuperLikes");
            DropTable("dbo.UserLikes");
            DropTable("dbo.UserSuperLikeLogs");
            DropTable("dbo.Posts");
            DropTable("dbo.Images");
            DropTable("dbo.Users");
            DropTable("dbo.DailySuggestionLogs");
            DropTable("dbo.Cities");
        }
    }
}
