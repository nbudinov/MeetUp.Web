namespace MeetUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserImageExtensionAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Extension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Extension");
        }
    }
}
