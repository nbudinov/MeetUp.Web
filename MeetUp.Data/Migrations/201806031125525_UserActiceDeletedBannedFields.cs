namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserActiceDeletedBannedFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Active", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Deleted", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Banned", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Banned");
            DropColumn("dbo.Users", "Deleted");
            DropColumn("dbo.Users", "Active");
        }
    }
}
