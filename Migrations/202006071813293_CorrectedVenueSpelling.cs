namespace ChooseEvent2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedVenueSpelling : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Gigs", "Vanue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gigs", "Vanue", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Gigs", "Venue");
        }
    }
}
