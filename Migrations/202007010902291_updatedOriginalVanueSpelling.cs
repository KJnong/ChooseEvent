namespace ChooseEvent2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedOriginalVanueSpelling : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "OriginalVanue", c => c.String());
            DropColumn("dbo.Notifications", "OiginalVanue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "OiginalVanue", c => c.String());
            DropColumn("dbo.Notifications", "OriginalVanue");
        }
    }
}
