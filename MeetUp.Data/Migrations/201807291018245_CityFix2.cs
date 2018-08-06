namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityFix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId" });
            AlterColumn("dbo.Users", "CityId", c => c.Int());
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId" });
            AlterColumn("dbo.Users", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
