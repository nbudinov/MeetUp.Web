namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreateTime", c => c.DateTime());
            AddColumn("dbo.Users", "LastOnline", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastOnline");
            DropColumn("dbo.Users", "CreateTime");
        }
    }
}
